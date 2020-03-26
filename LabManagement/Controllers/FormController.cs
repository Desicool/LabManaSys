using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DatabaseConnector.DAO.Utils;
using LabManagement.Authorization;
using LabManagement.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly ILogger<FormController> _logger;
        public FormController(ILogger<FormController> logger)
        {
            _logger = logger;
        }
        [HttpPost("declear")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Declear([FromBody] PostDeclarationFormParam param)
        {
            var certification = HttpContext.Request.Headers["certification"];
            var success = UserRoleCache.TryGetUserRole(certification, out _);
            if (!success)
            {
                return NotFound("try again");
            }
            try
            {
                RpcWrapper.CallServiceByPost("/api/entity/declarationform",
                    JsonSerializer.Serialize(param));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}