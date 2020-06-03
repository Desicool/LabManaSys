using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseConnector.DAO;
using DatabaseConnector.DAO.Entity;
using DatabaseConnector.DAO.FormData;
using DatabaseConnector.DAO.Utils;
using DatabaseConnector.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatabaseConnector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly ILogger<ClaimController> _logger;
        private readonly LabContext _context;
        public ClaimController(LabContext context, ILogger<ClaimController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetClaimFormDetail([FromQuery] int formid)
        {
            _logger.LogInformation("Query ClaimFormDetail with formid:{formid}", formid);
            var tmp = _context.ClaimFormChemicalMap
                .Where(u => u.ClaimFormId == formid)
                .Include(u => u.Chemical)
                .Include(u => u.ClaimForm)
                .ToList();
            var ret = new PostClaimFormParam
            {
                Form = tmp[0].ClaimForm,
                Chemicals = tmp.Select(t => t.Chemical).ToList()
            };
            return Ok(ret);
        }
        [HttpGet("person")]
        public IActionResult GetClaimForm([FromQuery] int userid)
        {
            var ret = new List<PostClaimFormParam>();
            var tmp = _context.ClaimFormChemicalMap
                .Include(u => u.Chemical)
                .Include(u => u.ClaimForm)
                .Where(u => u.ClaimForm.UserId == userid)
                .GroupBy(u => u.ClaimFormId)
                .ToList();
            foreach (var item in tmp)
            {
                var group = item.ToList();
                ret.Add(new PostClaimFormParam
                {
                    Form = group[0].ClaimForm,
                    Chemicals = group.Select(u => u.Chemical).ToList()
                });
            }
            return Ok(ret);
        }
        [HttpGet("lab")]
        public IActionResult GetLabClaimForms([FromQuery] int labid)
        {
            var ret = new List<PostClaimFormParam>();
            var tmp = _context.ClaimFormChemicalMap
                .Include(u => u.Chemical)
                .Include(u => u.ClaimForm)
                .Where(u => u.ClaimForm.LabId == labid)
                .GroupBy(u => u.ClaimFormId)
                .ToList();
            foreach (var item in tmp)
            {
                var group = item.ToList();
                ret.Add(new PostClaimFormParam
                {
                    Form = group[0].ClaimForm,
                    Chemicals = group.Select(u => u.Chemical).ToList()
                });
            }
            return Ok(ret);
        }
        [HttpPost("apply")]
        public IActionResult PostClaimForm([FromBody] PostClaimFormParam param)
        {
            _logger.LogInformation("Get posted claim form. formid: {formid}",param.Form.Id);
            var form = param.Form;
            form.State = Utils.FormState.InProcess;
            _context.ClaimForms.Add(form);
            _context.SaveChanges();
            // data format
            foreach(var chemical in param.Chemicals)
            {
                var entity = new ClaimFormChemical
                {
                    ChemicalId = chemical.ChemicalId,
                    ClaimForm = form,
                    ClaimFormId = form.Id
                };
                _context.ClaimFormChemicalMap.Add(entity);
                var dbChemical = _context.Chemicals.Where(c => c.ChemicalId == chemical.ChemicalId).Single();
                dbChemical.State = ChemicalState.InApplication;
            }
            var roles = _context.Roles
                .Where(r => r.RoleName == "LabTeacher" && r.LabId.HasValue? r.LabId == form.LabId:false)
                .ToList();
            _logger.LogInformation("Send message to {0} roles.", roles.Count);
            _logger.LogInformation("Role list: ");
            foreach (var role in roles)
            {
                _logger.LogInformation("Role id: {0}, name: {1}", role.RoleId, role.RoleName);
                // send message to role 
                var msg = new NotificationMessage
                {
                    FormId = form.Id,
                    FormType = FormType.ClaimForm,
                    IsSolved = false,
                    RoleId = role.RoleId
                };
                _context.NotificationMessages.Add(msg);
            }
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("approve")]
        public IActionResult ApproveClaim([FromBody] SolveFormParam param)
        {
            // change state
            var formlist = _context.ClaimFormChemicalMap
                .Where(u => u.ClaimFormId == param.FormId)
                .Include(u=>u.Chemical)
                .Include(u=>u.ClaimForm)
                .ToList();
            if(formlist.Count <= 0)
            {
                _logger.LogError("ClaimForm not found");
                throw new NullReferenceException();
            }
            if (formlist[0].ClaimForm.State != FormState.InProcess)
            {
                return NotFound("已有其他老师处理过该申请。");
            }
            formlist[0].ClaimForm.HandlerId = param.UserId;
            formlist[0].ClaimForm.HandlerName = param.UserName;
            formlist[0].ClaimForm.State = Utils.FormState.Approved;
            foreach(var item in formlist)
            {
                item.Chemical.State = ChemicalState.InUse;
            }
            // change msg status
            var oldmsg = _context.NotificationMessages.Where(m => m.FormId == param.FormId && m.FormType == FormType.ClaimForm).ToList();
            if (oldmsg.Count != 0)
            {
                foreach (var m in oldmsg)
                {
                    m.IsSolved = true;
                }
            }
            // send msg
            var msgs = _context.WorkFlowStatusChangeMessages.Where(u => u.RelatedId == param.FormId).ToList();
            if (msgs.Count > 0)
            {
                foreach (var msg in msgs)
                {
                    msg.IsRead = false;
                }
            }
            else
            {
                var msg = new StatusChangeMessage
                {
                    RelatedId = param.FormId,
                    RelatedType = RelatedTypeEnum.ClaimForm,
                    IsRead = false,
                    UserId = formlist[0].ClaimForm.UserId
                };
                _context.WorkFlowStatusChangeMessages.Add(msg);
            }
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("reject")]
        public IActionResult RejectClaim([FromBody] SolveFormParam param)
        {
            var form = _context.ClaimForms.Where(u => u.Id == param.FormId).Single();
            if (form.State != FormState.InProcess)
            {
                return NotFound("已有其他老师处理过该申请。");
            }
            form.State = Utils.FormState.Rejected;
            form.HandlerName = param.UserName;
            // change msg status
            var oldmsg = _context.NotificationMessages.Where(m => m.FormId == param.FormId && m.FormType == FormType.ClaimForm).ToList();
            if (oldmsg.Count != 0)
            {
                foreach (var m in oldmsg)
                {
                    m.IsSolved = true;
                }
            }
            // send msg
            var msgs = _context.WorkFlowStatusChangeMessages.Where(u => u.RelatedId == param.FormId).ToList();
            if (msgs.Count > 0)
            {
                foreach (var msg in msgs)
                {
                    msg.IsRead = false;
                }
            }
            else
            {
                var msg = new StatusChangeMessage
                {
                    RelatedId = param.FormId,
                    RelatedType = RelatedTypeEnum.ClaimForm,
                    IsRead = false,
                    UserId = form.UserId
                };
                _context.WorkFlowStatusChangeMessages.Add(msg);
            }
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("return")]
        public IActionResult ReturnChemicals([FromBody] SolveFormParam param)
        {
            // change state
            var formlist = _context.ClaimFormChemicalMap
                .Where(u => u.ClaimFormId == param.FormId)
                .Include(u => u.Chemical)
                .Include(u => u.ClaimForm)
                .ToList();
            if (formlist.Count <= 0)
            {
                _logger.LogError("ClaimForm not found");
                throw new NullReferenceException();
            }
            formlist[0].ClaimForm.RealReturnTime = DateTime.Now;
            foreach (var item in formlist)
            {
                item.ClaimForm.State = FormState.Returned;
                item.Chemical.State = ChemicalState.Lab;
            }
            return Ok();
        }

        public int GetNotifyRoleId(string roleName, int? labId)
        {
            return _context.Roles
                .Where(r =>
                    r.RoleName == roleName
                    && (r.LabId.HasValue ? r.LabId == labId : true))
                .Select(r => r.RoleId).Single();
        }
    }
}