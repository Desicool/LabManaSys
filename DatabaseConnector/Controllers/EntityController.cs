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
            return _context.Chemicals.Where(c => c.LabId == labId && (c.State == ChemicalState.InUse || c.State == ChemicalState.Lab)).ToList();
        }
        [HttpGet("user/chemicals")]
        public List<Chemical> GetUserChemicals(int userid)
        {
            var query = from x in _context.ClaimFormChemicalMap.Include(u => u.ClaimForm).Include(u => u.Chemical)
                        where x.ClaimForm.State == FormState.Approved && x.ClaimForm.UserId == userid
                        select x.Chemical;
            return query.ToList();
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
            _logger.LogInformation("Get messages for user: {userid}", userid);
            var ur = _context.UserRoleRelation
                .Where(u => u.UserId == userid)
                .ToList();
            var ret = new MsgResult
            {
                DeclarationForms = new List<DeclarationForm>(),
                ClaimForms = new List<ClaimForm>(),
                FinancialForms = new List<FinancialForm>()
            };
            /* var msgs = _context.NotificationMessages
                 .Where(v=>v.IsSolved == false)
                 .GroupBy(v=>v.FormType)
                 .ToList();*/

            var cf = _context.ClaimForms.FromSqlRaw("select * from dbo.ClaimForm cf " + 
                "where cf.Id in ("+
                "select b.FormId from dbo.NotificationMessage b "+
                "where b.IsSolved = 0 and b.FormType = {0} and b.RoleId in ("+
                "select RoleId from dbo.UserRole "+
                "where UserId = {1}))", FormType.ClaimForm, userid).ToList();
            ret.ClaimForms.AddRange(cf);
            var df = _context.DeclarationForms.FromSqlRaw("select * from dbo.DeclarationForm cf " +
                "where cf.Id in (" +
                "select b.FormId from dbo.NotificationMessage b " +
                "where b.IsSolved = 0 and b.FormType = {0} and b.RoleId in (" +
                "select RoleId from dbo.UserRole " +
                "where UserId = {1}))", FormType.DeclarationForm, userid).ToList();
            ret.DeclarationForms.AddRange(df);
            var ff = _context.FinancialForms.FromSqlRaw("select * from dbo.FinancialForm cf " +
                "where cf.Id in (" +
                "select b.FormId from dbo.NotificationMessage b " +
                "where b.IsSolved = 0 and b.FormType = {0} and b.RoleId in (" +
                "select RoleId from dbo.UserRole " +
                "where UserId = {1}))", FormType.FinancialForm, userid).ToList();
            ret.FinancialForms.AddRange(ff);
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
            var cf = _context.ClaimForms
                .FromSqlRaw("select * from dbo.ClaimForm cf " +
                    "where cf.Id in (" +
                    "select b.RelatedId from dbo.StatusChangeMessage b " +
                    "where b.IsRead = 0 and b.RelatedType = {0} and b.userId={1})",
                    RelatedTypeEnum.ClaimForm, userid)
                .ToList();
            ret.ClaimForms.AddRange(cf);

            var wf = _context.WorkFlows
                .FromSqlRaw("select * from dbo.WorkFlow cf " +
                    "where cf.Id in (" +
                    "select b.RelatedId from dbo.StatusChangeMessage b " +
                    "where b.IsRead = 0 and b.RelatedType = {0} and b.userId={1})",
                    RelatedTypeEnum.WorkFlow, userid)
                .ToList();
            ret.WorkFlows.AddRange(wf);

            return ret;
        }
        [HttpPost("notify")]
        public IActionResult UpdateNotifyStatus([FromBody]NotifyUpdateParam param)
        {
            _logger.LogInformation("update notify infos. relatedid: {relatedid}", param.RelatedId);
            var notify = _context.WorkFlowStatusChangeMessages
                .Where(u => u.RelatedId == param.RelatedId && u.UserId == param.UserId && u.RelatedType == param.RelatedType)
                .Single();
            notify.IsRead = true;
            _context.SaveChanges();
            return Ok();
        }
    }
}