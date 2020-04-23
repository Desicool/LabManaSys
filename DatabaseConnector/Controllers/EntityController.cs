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
            if (type == "userid")
                return _context.WorkFlows.Where(w => w.UserId == id)
                    .ToList();
            if (type == "labid")
                return _context.WorkFlows
                    .Where(u => _context.Users.Any(x => x.LabId == id))
                    .ToList();
            throw new ArgumentOutOfRangeException();
        }
        [HttpGet("workflow")]
        public WorkFlow GetWorkFlowById([FromQuery] int workflowid)
        {
            return _context.WorkFlows.Where(w=>w.Id == workflowid)
                    .Include(w => w.Chemicals).Single();
            throw new ArgumentOutOfRangeException();
        }
        [HttpPost("chemical/discard")]
        public IActionResult DiscardChemical([FromBody] List<Chemical> chemicals)
        {
            var list = _context.Chemicals
                .Where(u => chemicals.Exists(v => v.ChemicalId == u.ChemicalId))
                .ToList();
            _context.Chemicals.RemoveRange(list);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("msg")]
        public MsgResult GetMessages([FromQuery]int userid)
        {
            var ur = _context.UserRoleRelation
                .Where(u => u.UserId == userid)
                .ToList();
            var ret = new MsgResult
            {
                DeclarationForms = new List<DeclarationForm>(),
                ClaimForms = new List<ClaimForm>(),
                FinancialForms = new List<FinancialForm>()
            };
            foreach(var role in ur)
            {
                var msgs = _context.NotificationMessages
                    .Where(u => u.RoleId == role.RoleId)
                    .GroupBy(u => u.FormType)
                    .ToList();
                foreach(var msg in msgs)
                {
                    var group = msg.ToList();
                    switch (msg.Key)
                    {
                        case FormType.ClaimForm:
                            {
                                var t = _context.ClaimForms
                                    .Where(c => group.Exists(g=>g.FormId == c.Id))
                                    .ToList();
                                ret.ClaimForms.AddRange(t);
                                break;
                            }
                        case FormType.DeclarationForm:
                            {
                                var t = _context.DeclarationForms
                                    .Where(c => group.Exists(g => g.FormId == c.Id))
                                    .ToList();
                                ret.DeclarationForms.AddRange(t);
                                break;
                            }
                        case FormType.FinancialForm:
                            {
                                var t = _context.FinancialForms
                                    .Where(c => group.Exists(g => g.FormId == c.Id))
                                    .ToList();
                                ret.FinancialForms.AddRange(t);
                                break;
                            }
                    }
                }
            }
            ret.DeclarationForms.Sort();
            ret.ClaimForms.Sort();
            ret.FinancialForms.Sort();
            return ret;
        }
        [HttpGet("notify")]
        public NotifyResult GetNotifies([FromQuery]int userid)
        {
            var ur = _context.Users
                .Where(u => u.UserId == userid)
                .Single();
            var ret = new NotifyResult
            {
                ClaimForms = new List<ClaimForm>(),
                WorkFlows = new List<WorkFlow>()
            };

            var msgs = _context.WorkFlowStatusChangeMessages
                .Where(u => u.UserId == userid)
                .GroupBy(u => u.RelatedType)
                .ToList();
            foreach (var msg in msgs)
            {
                var group = msg.ToList();
                switch (msg.Key)
                {
                    case RelatedTypeEnum.ClaimForm:
                        {
                            var t = _context.ClaimForms
                                .Where(c => group.Exists(g => g.RelatedId == c.Id))
                                .ToList();
                            ret.ClaimForms.AddRange(t);
                            break;
                        }
                    case RelatedTypeEnum.WorkFlow:
                        {
                            var t = _context.WorkFlows
                                .Where(c => group.Exists(g => g.RelatedId == c.Id))
                                .ToList();
                            ret.WorkFlows.AddRange(t);
                            break;
                        }
                }
            }

            return ret;
        }
    }
}