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

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            try
            {
                var response = RpcWrapper.CallServiceWithResult(
                    "/api/userrole", $"username={username}");
                var result = JsonSerializer.Deserialize<UserRoleResult>(response);
                if (password == result.User.UserPassword)
                {
                    string certification = Guid.NewGuid().ToString();
                    //UserRoleCache.AddUserRoleToCache(certification, result);
                    //return Ok(certification);
                    // For easy debug
                    UserRoleCache.AddUserRoleToCache("123", result);
                    return Ok("123");
                }
                return Ok("Wrong username or password.");
            }
            catch (Exception)
            {
                return Ok("Server internal failed.");
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