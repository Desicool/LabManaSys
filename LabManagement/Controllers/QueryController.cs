using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabManagement.Authorization;
using LabManagement.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using DatabaseConnector.DAO.Entity;

namespace LabManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class QueryController : ControllerBase
    {
        private readonly ILogger<QueryController> _logger;

        public QueryController(ILogger<QueryController> logger)
        {
            _logger = logger;
        }

        [HttpGet("chemicals")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult QueryLabChemicals()
        {
            var certification = HttpContext.Request.Headers["certification"];
            var success = UserRoleCache.TryGetUserRole(certification, out var userRole);
            if (!success)
            {
                return NotFound("try again");
            }
            int labId = userRole.User.LabId;
            try
            {
                var response = RpcWrapper.CallServiceWithResult("/api/entity/chemicals", $"labId={labId}");
                var res = JsonSerializer.Deserialize<List<Chemical>>(response);
                return Ok(res);
            }
            catch (JsonException)
            {
                return BadRequest("internal error");
            }
            catch (Exception)
            {
                return NotFound("try again");
            }
        }
    }
}
