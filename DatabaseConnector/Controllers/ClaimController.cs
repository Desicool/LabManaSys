using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseConnector.DAO;
using DatabaseConnector.DAO.Entity;
using DatabaseConnector.DAO.FormData;
using DatabaseConnector.DAO.Utils;
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
            var form = param.Form;
            form.State = Utils.FormState.InProcess;
            _context.ClaimForms.Add(form);
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
            }
            var role = _context.Roles
                .Where(r => r.RoleName == "LabTeacher" && r.LabId.HasValue? r.LabId == form.LabId:false)
                .First();
            // send message to role 
            var msg = new NotificationMessage
            {
                FormId = form.Id,
                FormType = FormType.ClaimForm,
                IsSolved = false,
                RoleId = role.RoleId
            };
            _context.NotificationMessages.Add(msg);
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
            formlist[0].ClaimForm.HandlerId = param.UserId;
            formlist[0].ClaimForm.HandlerName = param.UserName;
            formlist[0].ClaimForm.State = Utils.FormState.Approved;
            foreach(var item in formlist)
            {
                item.Chemical.State = ChemicalState.InUse;
            }
            // send msg
            var roleid = GetNotifyRoleId("Applicant",null);
            var msg = new NotificationMessage
            {
                FormId = param.FormId,
                FormType = FormType.ClaimForm,
                IsSolved = false,
                RoleId = roleid
            };
            _context.NotificationMessages.Add(msg);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("reject")]
        public IActionResult RejectClaim([FromBody] SolveFormParam param)
        {
            var form = _context.ClaimForms.Where(u => u.Id == param.FormId).Single();
            form.State = Utils.FormState.Rejected;
            form.HandlerName = param.UserName;
            // send msg
            var roleid = GetNotifyRoleId("Applicant", null);
            var msg = new NotificationMessage
            {
                FormId = param.FormId,
                FormType = FormType.ClaimForm,
                IsSolved = false,
                RoleId = roleid
            };
            _context.NotificationMessages.Add(msg);
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