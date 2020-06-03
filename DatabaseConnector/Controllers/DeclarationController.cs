using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseConnector.DAO;
using DatabaseConnector.DAO.Entity;
using DatabaseConnector.DAO.Utils;
using DatabaseConnector.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DatabaseConnector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeclarationController : ControllerBase
    {
        private readonly ILogger<DeclarationController> _logger;
        private readonly LabContext _context;
        private readonly StateUtil util;
        public DeclarationController(LabContext context, ILogger<DeclarationController> logger, StateUtil state)
        {
            _context = context;
            _logger = logger;
            util = state;
        }
        [HttpGet]
        public IActionResult GetDeclarationForms([FromQuery] long formid)
        {
            var form = _context.DeclarationForms.Where(u => u.Id == formid).Single();
            var ret = new PostDeclarationFormParam()
            {
                Form = form,
                Chemicals = _context.Chemicals.Where(c=>c.WorkFlowId == form.WorkFlowId).ToList()
            };
            return Ok(ret);
        }

        [HttpGet("workflow")]
        public IActionResult GetDeclarationFormByWorkFlow([FromQuery] long workflowid)
        {
            var form = _context.DeclarationForms.Where(u => u.WorkFlowId == workflowid).Single();
            var ret = new PostDeclarationFormParam()
            {
                Form = form,
                Chemicals = _context.Chemicals.Where(c => c.WorkFlowId == form.WorkFlowId).ToList()
            };
            return Ok(ret);
        }
        [HttpGet("person")]
        public IActionResult GetDeclarationForm([FromQuery] int userid)
        {
            return Ok(_context.DeclarationForms.Where(u => u.UserId == userid).ToList());
        }
        [HttpGet("lab")]
        public IActionResult GetLabDeclarationForms([FromQuery] int labid)
        {
            return Ok(_context.DeclarationForms.Where(u => u.LabId == labid).ToList());
        }
        [HttpPost("apply")]
        public IActionResult PostDeclarationForm([FromBody] PostDeclarationFormParam param)
        {
            var form = param.Form;
            form.State = FormState.InProcess;
            // first create a workflow
            var workflow = new WorkFlow
            {
                UserId = param.Form.UserId,
                StartTime = DateTime.Now,
                State = "declearing",
                Chemicals = param.Chemicals,
                Description = param.Form.Reason,
                UserName = param.Form.UserName
            };
            _context.WorkFlows.Add(workflow);
            _context.SaveChanges();
            // create form_workflow_relationship
            form.WorkFlowId = workflow.Id;
            _context.DeclarationForms.Add(form);
            _context.SaveChanges();
            var role = GetNotifyRoleId(util.StateRoute[workflow.State].RoleName, param.Form.LabId);
            // send message to role 
            var msg = new NotificationMessage
            {
                FormId = form.Id,
                FormType = FormType.DeclarationForm,
                IsSolved = false,
                RoleId = role
            };
            _context.NotificationMessages.Add(msg);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("approve")]
        public IActionResult ApproveDeclaration([FromBody] SolveFormParam param)
        {
            // change state
            var form = _context.DeclarationForms.Where(u => u.Id == param.FormId).Single();
            if(form.State != FormState.InProcess)
            {
                return NotFound("已有其他老师处理过该申请。");
            }
            form.HandlerId = param.UserId;
            form.HandlerName = param.UserName;
            form.State = FormState.Approved;
            var workflow = _context.WorkFlows.Where(u => u.Id == form.WorkFlowId).Single();
            var data = util.StateRoute[workflow.State];
            workflow.State = data.Next[1];
            data = util.StateRoute[workflow.State];
            // change msg status
            var oldmsg = _context.NotificationMessages.Where(m => m.FormId == param.FormId && m.FormType == FormType.DeclarationForm).ToList();
            if (oldmsg.Count != 0)
            {
                foreach (var m in oldmsg)
                {
                    m.IsSolved = true;
                }
            }
            // send msg
            var msgs = _context.WorkFlowStatusChangeMessages.Where(u => u.RelatedId == workflow.Id).ToList();
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
                    RelatedId = workflow.Id,
                    RelatedType = RelatedTypeEnum.WorkFlow,
                    IsRead = false,
                    UserId = workflow.UserId
                };
                _context.WorkFlowStatusChangeMessages.Add(msg);
            }
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("reject")]
        public IActionResult RejectDeclaration([FromBody] SolveFormParam param)
        {
            // change state
            var form = _context.DeclarationForms.Where(u => u.Id == param.FormId).Single();
            if (form.State != FormState.InProcess)
            {
                return NotFound("已有其他老师处理过该申请。");
            }
            form.HandlerId = param.UserId;
            form.HandlerName = param.UserName;
            form.State = FormState.Rejected;
            var workflow = _context.WorkFlows.Where(u => u.Id == form.WorkFlowId).Single();
            var data = util.StateRoute[workflow.State];
            workflow.State = data.Next[0];
            // change msg status
            var oldmsg = _context.NotificationMessages.Where(m => m.FormId == param.FormId && m.FormType == FormType.DeclarationForm).ToList();
            if (oldmsg.Count != 0)
            {
                foreach (var m in oldmsg)
                {
                    m.IsSolved = true;
                }
            }
            // send msg
            var msgs = _context.WorkFlowStatusChangeMessages.Where(u => u.RelatedId == workflow.Id).ToList();
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
                    RelatedId = workflow.Id,
                    RelatedType = RelatedTypeEnum.WorkFlow,
                    IsRead = false,
                    UserId = workflow.UserId
                };
                _context.WorkFlowStatusChangeMessages.Add(msg);
            }
            _context.SaveChanges();
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