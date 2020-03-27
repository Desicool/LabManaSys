using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        [HttpGet("workflows")]
        public List<WorkFlow> GetWorkFlowsByUserId([FromQuery] int id, [FromQuery] string type)
        {
            type = type.ToLower();
            if(type == "userid")
                return _context.WorkFlows.Where(w=>w.UserId == id).ToList();
            if (type == "labid")
                return _context.WorkFlows
                    .Where(u => _context.Users.Any(x => x.LabId == id))
                    .ToList();
            throw new ArgumentOutOfRangeException();
        }
        [HttpPost("financialform")]
        public IActionResult PostFinancialForm([FromBody] PostFinancialFormParam param)
        {
            return Ok();
        }
    }
}