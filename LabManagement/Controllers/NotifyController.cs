using DatabaseConnector.DAO.Utils;
using LabManagement.Authorization;
using LabManagement.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotifyController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        public NotifyController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult ReadStatusChange([FromBody] NotifyUpdateParam param)
        {
            var certification = HttpContext.Request.Headers["certification"];
            if (UserRoleCache.TryGetUserRole(certification, out var userRole))
            {
                param.UserId = userRole.User.UserId;
                try
                {
                    var response = RpcWrapper.CallServiceByPost("/api/declaration/workflow",
                        JsonSerializer.Serialize(param));
                    return Ok();
                }
                catch (Exception)
                {
                    return NotFound("try again");
                }
            }
            return Unauthorized();
        }
    }
}
