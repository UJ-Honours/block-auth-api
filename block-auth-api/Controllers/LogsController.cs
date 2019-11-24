using block_auth_api.Orchestration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;
using OrchestrationLibrary.LogsOrchestration;
using System;

namespace block_auth_api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]"),Authorize]
    public class LogsController : Controller
    {
        private readonly ILogsOrchestration _LCO;

        public LogsController(ILogsOrchestration lco)
        {
            _LCO = lco;
        }

        [HttpGet]
        [Route("get_logs")]
        public ActionResult GetLogs()
        {
            try
            {
                var logsDictionary = _LCO.GetLogs();

                if (logsDictionary != null)
                {
                    return Ok(logsDictionary);
                }
                else
                {
                    return Ok("No logs found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add_log")]
        public ActionResult AddLog([FromBody] Log log)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Passed");
                }

                _LCO.AddLog(log);

                return Ok(log);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}