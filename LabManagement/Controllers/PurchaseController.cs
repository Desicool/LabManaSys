using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using DatabaseConnector.DAO.Entity;
using LabManagement.Authorization;
using LabManagement.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        public PurchaseController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Purchase([FromQuery] int workflowid)
        {
            try
            {
                var response = HttpWrapper.CallServiceByGet("/api/entity/purchase",
                    $"workflowid={workflowid}");
                if (!response.IsSuccessCode)
                {
                    return NotFound("internal error");
                }
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("try again");
            }
        }
        [HttpPost("destroy")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Destroy([FromBody] Chemical chemical)
        {
            _logger.LogInformation("destroy chemical id: {1}", chemical.ChemicalId);
            try
            {
                var response = HttpWrapper.CallServiceByPost("/api/entity/chemical/discard",
                    JsonSerializer.Serialize(chemical));
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("try again");
            }
        }
    }
}