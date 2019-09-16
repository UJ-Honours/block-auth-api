using block_auth_api.Orchestration;
using Microsoft.AspNetCore.Mvc;
using System;

namespace block_auth_api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskOrchestration _TO;
        public TaskController(ITaskOrchestration to)
        {
            _TO = to;
        }

        [HttpGet]
        [Route("get_tasks")]
        public ActionResult GetTasks()
        {
            try
            {
                var tasksDictionary = _TO.GetTasks();
                return Ok(tasksDictionary);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("create_task")]
        public ActionResult CreateTask()
        {
            try
            {
                _TO.CreateTask();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("toggle_task")]
        public ActionResult ToggleTask()
        {
            try
            {
                _TO.ToggleTask();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
