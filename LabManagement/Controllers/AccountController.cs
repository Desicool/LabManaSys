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
        public IActionResult Login([FromForm] string username,[FromForm] string password)
        {
            var response = RpcWrapper.CallServiceWithResult(
                "/api/userrole", $"username={username}");
            if(response.StatusCode == HttpStatusCode.OK)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                var result = JsonSerializer.Deserialize<UserRoleResult>(responseBody);
                if (password == result.User.UserPassword)
                {
                    string certification = Guid.NewGuid().ToString();
                    UserRoleCache.AddUserRoleToCache(certification, result);
                    return Ok(certification);
                }
            }
            return Ok("failed");
        }
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Logout([FromBody] LogoutParam param)
        {
            var certification = HttpContext.Request.Headers["certification"];
            UserRoleCache.TryGetUserRole(certification, out var userRole);
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