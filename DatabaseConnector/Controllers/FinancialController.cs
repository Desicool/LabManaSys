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
    public class FinancialController : ControllerBase
    {
        private readonly ILogger<FinancialController> _logger;
        private readonly LabContext _context;
        private readonly StateUtil util;
        public FinancialController(LabContext context, ILogger<FinancialController> logger, StateUtil state)
        {
            _context = context;
            _logger = logger;
            util = state;
        }
        [HttpGet("workflow")]
        public IActionResult GetFinancialForms([FromQuery] long formid)
        {
            return Ok(_context.FinancialForms.Where(u => u.Id == formid).ToList());
        }
        [HttpGet("lab")]
        public IActionResult GetLabFinancialForms([FromQuery] int labid)
        {
            return Ok(_context.FinancialForms.Where(u => u.LabId == labid).ToList());
        }
        [HttpGet("person")]
        public IActionResult GetFinancialForm([FromQuery] int userid)
        {
            return Ok(_context.FinancialForms.Where(u => u.UserId == userid).ToList());
        }
        [HttpPost("apply")]
        public IActionResult PostFinancialForm([FromBody] PostFinancialFormParam param)
        {
            var form = param.Form;
            form.State = FormState.InProcess;
            _context.FinancialForms.Add(form);
            // workflow state should be at securityOk
            // move to financial
            var workflow = _context.WorkFlows.Where(u => u.Id == param.Form.WorkFlowId).Single();
            workflow.State = util.StateRoute[workflow.State].Next[1];
            var role = _context.Roles
                .Where(r => r.RoleName == util.StateRoute[workflow.State].RoleName)
                .Single();
            // send message to role 
            var msg = new NotificationMessage
            {
                FormId = form.Id,
                FormType = FormType.DeclarationForm,
                IsSolved = false,
                RoleId = role.RoleId
            };
            _context.NotificationMessages.Add(msg);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("approve")]
        public IActionResult ApproveFinancial([FromBody] SolveFormParam param)
        {
            // change state
            var form = _context.FinancialForms.Where(u => u.Id == param.FormId).Single();
            if (form.State != FormState.InProcess)
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
            var oldmsg = _context.NotificationMessages.Where(m => m.FormId == param.FormId).Single();
            oldmsg.IsSolved = true;
            // send msg
            var msgs = _context.WorkFlowStatusChangeMessages.Where(u => u.RelatedId == workflow.Id).ToList();
            if (msgs.Count > 0)
            {
                foreach(var msg in msgs)
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
        public IActionResult RejectFinancial([FromBody] SolveFormParam param)
        {
            // change state
            var form = _context.FinancialForms.Where(u => u.Id == param.FormId).Single();
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
            var oldmsg = _context.NotificationMessages.Where(m => m.FormId == param.FormId).Single();
            oldmsg.IsSolved = true;
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