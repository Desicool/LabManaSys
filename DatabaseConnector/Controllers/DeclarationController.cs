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
        [HttpPost("apply")]
        public IActionResult PostDeclarationForm([FromBody] PostDeclarationFormParam param)
        {
            var form = param.Form;
            // first create a workflow
            var workflow = new WorkFlow
            {
                UserId = param.Form.UserId,
                StartTime = DateTime.Now,
                State = "declearing",
                Chemicals = param.Chemicals,
            };
            _context.WorkFlows.Add(workflow);
            // create form_workflow_relationship
            form.WorkFlowId = workflow.Id;
            _context.DeclarationForms.Add(form);
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
        public IActionResult ApproveDeclaration([FromBody] ApproveDeclarationParam param)
        {
            return Ok();
        }
    }
}