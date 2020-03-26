using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DatabaseConnector.DAO;
using DatabaseConnector.DAO.Entity;
using DatabaseConnector.DAO.FormData;
using DatabaseConnector.DAO.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DatabaseConnector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly LabContext _context;
        private readonly ILogger<EntityController> _logger;
        public EntityController(LabContext context, ILogger<EntityController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet("chemicals")]
        public List<Chemical> GetChemicals(int labId)
        {
            return _context.Chemicals.Where(c => c.LabId == labId).ToList();
        }
        [HttpPost("declarationform")]
        public IActionResult PostDeclarationForm([FromBody] PostDeclarationFormParam param)
        {
            var form = param.Form;
            // first create a workflow
            var workflow = new WorkFlow
            {
                Applicant = form.Applicant,
                StartTime = DateTime.Now,
                State = "declearing",
                Chemicals = param.Chemicals
            };
            _context.WorkFlows.Add(workflow);
            // create form_workflow_relationship
            form.WorkFlowId = workflow.Id;
            _context.DeclarationForms.Add(form);
            // send message to role 
            _context.SaveChanges();
            return Ok();
        }


        [HttpPost("msg")]
        public IActionResult PostMessage()
        {
            var entity = new NotificationMessage
            {
                FormId = 1,
                FormType = FormType.DeclarationForm,
                IsSolved = false,
                RoleId = 1
            };
            _context.NotificationMessages.Add(entity);
            _context.SaveChanges();
            /*_context.NotificationMessages.ToList().ForEach(m =>
            {
                _logger.LogDebug(JsonSerializer.Serialize(m));
            });*/
            _context.Roles.ToList().ForEach(r =>
            {
                _logger.LogDebug(r.RoleName);
            });
            _logger.LogDebug("begin delete");
            _context.NotificationMessages.Remove(entity);
            _context.SaveChanges();
            _context.Roles.ToList().ForEach(r =>
            {
                _logger.LogDebug(r.RoleName);
            });
            return Ok();
        }
    }
}