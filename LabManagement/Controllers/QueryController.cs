﻿using System;
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
using DatabaseConnector.DAO.Utils;
using DatabaseConnector.DAO.FormData;

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
                var response = HttpWrapper.CallServiceByGet("/api/entity/chemicals", $"labId={labId}");
                if (!response.IsSuccessCode)
                {
                    return NotFound("try again");
                }
                var res = JsonSerializer.Deserialize<List<Chemical>>(response.Body);
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
        [HttpGet("user/chemicals")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult QueryUserChemicals()
        {
            var certification = HttpContext.Request.Headers["certification"];
            var success = UserRoleCache.TryGetUserRole(certification, out var userRole);
            if (!success)
            {
                return NotFound("try again");
            }
            int userid = userRole.User.UserId;
            _logger.LogInformation("query chemicals of user id: {1}", userid);
            try
            {
                var response = HttpWrapper.CallServiceByGet("/api/entity/user/chemicals", $"userid={userid}");
                if (!response.IsSuccessCode)
                {
                    return NotFound("try again");
                }
                var res = JsonSerializer.Deserialize<List<Chemical>>(response.Body);
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
        [HttpGet("claimform/chemicals")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult QueryClaimChemicals([FromQuery] long formid)
        {
            try
            {
                var response = HttpWrapper.CallServiceByGet("/api/entity/chemicals", $"labId={formid}");
                if (!response.IsSuccessCode)
                {
                    return NotFound("try again");
                }
                var res = JsonSerializer.Deserialize<List<Chemical>>(response.Body);
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
        [HttpGet("msg")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult QueryMessages()
        {
            var certification = HttpContext.Request.Headers["certification"];
            var success = UserRoleCache.TryGetUserRole(certification, out var userRole);
            if (!success)
            {
                return NotFound("try again");
            }
            int userId = userRole.User.UserId;
            try
            {
                var response = HttpWrapper.CallServiceByGet("/api/entity/msg", $"userid={userId}");
                if (!response.IsSuccessCode)
                {
                    return NotFound("try again");
                }
                var res = JsonSerializer.Deserialize<MsgResult>(response.Body);
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
        [HttpGet("notify")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetNotifyMessages()
        {
            var certification = HttpContext.Request.Headers["certification"];
            var success = UserRoleCache.TryGetUserRole(certification, out var userRole);
            if (!success)
            {
                return NotFound("try again");
            }
            int userId = userRole.User.UserId;
            try
            {
                var response = HttpWrapper.CallServiceByGet("/api/entity/notify", $"userid={userId}");
                if (!response.IsSuccessCode)
                {
                    return NotFound("try again");
                }
                var res = JsonSerializer.Deserialize<NotifyResult>(response.Body);
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
        [HttpGet("workflows")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult QueryWorkFlows()
        {
            var certification = HttpContext.Request.Headers["certification"];
            var success = UserRoleCache.TryGetUserRole(certification, out var userRole);
            if (!success)
            {
                return NotFound("try again");
            }
            int userId = userRole.User.UserId;
            try
            {
                var response = HttpWrapper.CallServiceByGet("/api/entity/workflows", $"id={userId}",$"type=userid");
                if (!response.IsSuccessCode)
                {
                    return NotFound("try again");
                }
                var res = JsonSerializer.Deserialize<List<WorkFlow>>(response.Body);
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
        [HttpGet("workflow/declaration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult QueryDeclarationByWorkFlow([FromQuery] int workflowId)
        {
            try
            {
                var response = HttpWrapper.CallServiceByGet("/api/declaration/workflow", $"workflowid={workflowId}");
                if (!response.IsSuccessCode)
                {
                    return NotFound("try again");
                }
                var res = JsonSerializer.Deserialize<PostDeclarationFormParam>(response.Body);
                return Ok(res.Form);
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
        [HttpGet("workflow/financial")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult QueryFinancialByWorkFlow([FromQuery] int workflowId)
        {
            try
            {
                var response = HttpWrapper.CallServiceByGet("/api/financial/workflow", $"workflowid={workflowId}");
                if (!response.IsSuccessCode)
                {
                    return NotFound("try again");
                }
                var res = JsonSerializer.Deserialize<List<FinancialForm>>(response.Body);
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
        [HttpGet("workflow")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult QueryWorkFlowById([FromQuery] int workflowId)
        {
            try
            {
                var response = HttpWrapper.CallServiceByGet("/api/entity/workflow", $"workflowid={workflowId}");
                if (!response.IsSuccessCode)
                {
                    return NotFound("try again");
                }
                var res = JsonSerializer.Deserialize<WorkFlow>(response.Body);
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
