using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using DatabaseConnector.DAO.Entity;
using LabManagement.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }
        [HttpPost("login")]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            _logger.LogInformation("Username: {username} try login.", username);
            try
            {
                var response = RpcWrapper.CallServiceByGet(
                    "/api/userrole", $"username={username}");
                var result = JsonSerializer.Deserialize<UserRoleResult>(response);
                if (password == result.User.UserPassword)
                {
                    string certification = Guid.NewGuid().ToString();
                    var ret = new LoginReturn
                    {
                        Success = true,
                        User = result.User,
                        Roles = result.Roles,
                        Certification = certification
                    };
                    //UserRoleCache.AddUserRoleToCache(certification, result);
                    //return Ok(certification);
                    // For easy debug
                    ret.Certification = "123";
                    UserRoleCache.AddUserRoleToCache("123", result);
                    return Ok(ret);
                }
                return Ok(new LoginReturn { Success = false });
            }
            catch (Exception e)
            {
                // not sure if this should be write here
                _logger.LogError(e.Message);
                _logger.LogError("Call database_connector failed.");
                throw e;
            }
        }
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Logout([FromBody] LogoutParam param)
        {
            var certification = HttpContext.Request.Headers["certification"];
            if(!UserRoleCache.TryGetUserRole(certification, out var userRole))
            {
                return Ok();
            }
            var user = userRole.User;
            // 二次校验，防止利用奇怪的方法把别人踢下线
            if (user.UserName == param.UserName && user.UserPassword == param.Password)
            {
                UserRoleCache.RemoveUserRoleFromCache(certification);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}