using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DatabaseConnector.DAO.FormData;
using DatabaseConnector.DAO.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EndPointJob.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly JobCore _core;

        public JobController(ILogger<JobController> logger, JobCore core)
        {
            _logger = logger;
            _core = core;
        }

        [HttpGet]
        public IActionResult StartJob()
        {
            Task.Run(RunJob);
            return Ok();
        }
        [HttpPost("declare")]
        public IActionResult AddDeclareJob([FromBody] DeclarationForm form)
        {
            _core.DeclearQueue.Enqueue(form);
            return Ok();
        }
        public void RunJob()
        {
            while (true)
            {
                while(_core.DeclearQueue.TryPeek(out var peek))
                {
                    if(peek.Deadline < DateTime.Now)
                    {
                        _logger.LogInformation("Start reject declaration form: {formid}.", peek.FormId);
                        var info = RpcWrapper.CallServiceByPost("/api/declaration/reject",
                            JsonSerializer.Serialize(new SolveFormParam
                            {
                                FormId = peek.FormId,
                                UserId = 7,
                                UserName = "system",
                            }));
                        _logger.LogInformation("Result: " + info);
                    }
                    else
                    {
                        break;
                    }
                    _core.DeclearQueue.Dequeue();
                }
                while (_core.FinancialQueue.TryPeek(out var peek))
                {
                    if (peek.Deadline < DateTime.Now)
                    {
                        _logger.LogInformation("Start reject financial form: {formid}.", peek.FormId);
                        var info = RpcWrapper.CallServiceByPost("/api/financial/reject",
                            JsonSerializer.Serialize(new SolveFormParam
                            {
                                FormId = peek.FormId,
                                UserId = 7,
                                UserName = "system",
                            }));
                        _logger.LogInformation("Result: " + info);
                    }
                    else
                    {
                        break;
                    }
                    _core.FinancialQueue.Dequeue();
                }
                while (_core.ClaimQueue.TryPeek(out var peek))
                {
                    if (peek.Deadline < DateTime.Now)
                    {
                        _logger.LogInformation("Start reject financial form: {formid}.", peek.FormId);
                        var info = RpcWrapper.CallServiceByPost("/api/financial/reject",
                            JsonSerializer.Serialize(new SolveFormParam
                            {
                                FormId = peek.FormId,
                                UserId = 7,
                                UserName = "system",
                            }));
                        _logger.LogInformation("Result: " + info);
                    }
                    else
                    {
                        break;
                    }
                    _core.ClaimQueue.Dequeue();
                }
                // check the queue every 5 minutes
                Thread.Sleep(300000);
            }
        }
    }
}
