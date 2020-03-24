using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseConnector.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatabaseConnector.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly LabContext _context;
        public AuthorizationController(ILogger<AuthorizationController> logger, LabContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("userrole")]
        public IActionResult Get(string username)
        {
            try
            {
                var _user = _context.Users.Single(u => u.UserName == username);
                var res = _context.UserRoleRelation
                    .Where(u => u.UserId == _user.UserId)
                    .Include(u=>u.Role)
                    .Select(u=>u.Role)
                    .ToList();
                return Ok(new { user = _user, roles = res });
            }
            catch(InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
