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
    [Authorize]
    public class FormController : ControllerBase
    {
        private readonly ILogger<FormController> _logger;
        public FormController(ILogger<FormController> logger)
        {
            _logger = logger;
        }
        #region Declear
        [HttpPost("declear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Declear([FromBody] PostDeclarationFormParam param)
        {
            try
            {
                param.Form.SubmitTime = DateTime.Now;
                RpcWrapper.CallServiceByPost("/api/declaration/apply",
                    JsonSerializer.Serialize(param));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        [HttpPost("declear/approve")]
        [Authorize(Role = "LabTeacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult ApproveDeclear([FromBody]SolveFormParam param)
        {
            if (UserRoleCache.TryGetUserRole(HttpContext.Request.Headers["certification"], out UserRoleResult result))
            {
                if (!result.Roles.Exists(r => r.LabId == param.LabId))
                    return Unauthorized();
            }
            try
            {
                RpcWrapper.CallServiceByPost("/api/declaration/approve",
                    JsonSerializer.Serialize(param));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        [HttpPost("declear/reject")]
        [Authorize(Role ="LabTeacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult RejectDeclear([FromBody]SolveFormParam param)
        {
            if (UserRoleCache.TryGetUserRole(HttpContext.Request.Headers["certification"], out UserRoleResult result))
            {
                if (!result.Roles.Exists(r => r.LabId == param.LabId))
                    return Unauthorized();
            }
            try
            {
                RpcWrapper.CallServiceByPost("/api/declaration/reject",
                    JsonSerializer.Serialize(param));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        [HttpGet("declear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetDeclearDetail ([FromQuery]int formid)
        {
            try
            {
                var response = RpcWrapper.CallServiceByGet("/api/declaration",$"formid={formid}");
                if (!response.IsSuccessCode)
                {
                    return NotFound("try again");
                }
                var res = JsonSerializer.Deserialize<PostDeclarationFormParam>(response.Body);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        #endregion
        #region Financial
        [HttpPost("financial")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Financial([FromBody] PostFinancialFormParam param)
        {
            try
            {
                param.Form.SubmitTime = DateTime.Now;
                RpcWrapper.CallServiceByPost("/api/financial/apply",
                    JsonSerializer.Serialize(param));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        [HttpPost("financial/approve")]
        [Authorize(Role = "FinancialTeacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult ApproveFinancial([FromBody] SolveFormParam param)
        {
            try
            {
                RpcWrapper.CallServiceByPost("/api/financial/approve",
                    JsonSerializer.Serialize(param));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        [HttpPost("financial/reject")]
        [Authorize(Role = "FinancialTeacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult RejectFinancial([FromBody] SolveFormParam param)
        {
            try
            {
                RpcWrapper.CallServiceByPost("/api/financial/reject",
                    JsonSerializer.Serialize(param));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        [HttpGet("financial")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetFinancialDetail([FromQuery]int formid)
        {
            try
            {
                var response = RpcWrapper.CallServiceByGet("/api/financial/workflow", $"formid={formid}");
                if (!response.IsSuccessCode)
                {
                    return NotFound("try again");
                }
                var res = JsonSerializer.Deserialize<PostDeclarationFormParam>(response.Body);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        #endregion
        #region claim
        [HttpPost("claim")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Claim([FromBody] PostClaimFormParam param)
        {
            _logger.LogInformation("Post claim form. formid: {formid}", param.Form.Id);
            _logger.LogInformation("With {count} chemicals.", param.Chemicals.Count);
            try
            {
                param.Form.SubmitTime = DateTime.Now;
                RpcWrapper.CallServiceByPost("/api/claim/apply",
                    JsonSerializer.Serialize(param));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        [HttpPost("claim/approve")]
        [Authorize(Role = "LabTeacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult ApproveClaim([FromBody] SolveFormParam param)
        {
            if (UserRoleCache.TryGetUserRole(HttpContext.Request.Headers["certification"], out UserRoleResult result))
            {
                if (!result.Roles.Exists(r => r.LabId == param.LabId))
                    return Unauthorized();
            }
            try
            {
                RpcWrapper.CallServiceByPost("/api/claim/approve",
                    JsonSerializer.Serialize(param));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        [HttpPost("claim/reject")]
        [Authorize(Role = "LabTeacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult RejectClaim([FromBody] SolveFormParam param)
        {
            if (UserRoleCache.TryGetUserRole(HttpContext.Request.Headers["certification"], out UserRoleResult result))
            {
                if (!result.Roles.Exists(r => r.LabId == param.LabId))
                    return Unauthorized();
            }
            try
            {
                RpcWrapper.CallServiceByPost("/api/claim/reject",
                    JsonSerializer.Serialize(param));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        [HttpPost("claim/reject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult ReturnChemicals([FromBody] SolveFormParam param)
        {
            try
            {
                RpcWrapper.CallServiceByPost("/api/claim/return",
                    JsonSerializer.Serialize(param));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        [HttpGet("claim")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetClaimDetail([FromQuery]int formid)
        {
            try
            {
                var response = RpcWrapper.CallServiceByGet("/api/claim", $"formid={formid}");
                if (!response.IsSuccessCode)
                {
                    return NotFound("try again");
                }
                var res = JsonSerializer.Deserialize<PostClaimFormParam>(response.Body);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        #endregion
    }
}