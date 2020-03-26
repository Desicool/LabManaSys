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

namespace DatabaseConnector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly LabContext _context;
        public EntityController(LabContext context)
        {
            _context = context;
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
            // not pretty sure if workflow has an id now.
            form.WorkFlowId = workflow.Id;
            _context.DeclarationForms.Add(form);
            _context.SaveChanges();
            return Ok();
        }
    }
}