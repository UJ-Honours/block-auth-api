using block_auth_api.Models;
using block_auth_api.Orchestration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;
using OrchestrationLibrary.LogsOrchestration;
using System;
using System.Collections.Generic;

namespace block_auth_api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]"),Authorize]
    public class UsersController : Controller
    {
        private readonly IUsersContractOrchestration _UCO;
        private readonly ILogsOrchestration _LCO;

        public UsersController(IUsersContractOrchestration uco, ILogsOrchestration lco)
        {
            _UCO = uco;
            _LCO = lco;
        }

        [HttpGet]
        [Route("get_users")]
        public ActionResult GetUsers()
        {
            try
            {
                var userList = _UCO.GetUsers();
                var usersDictionary = new Dictionary<string, List<User>>();
                foreach (var u in userList)
                {
                    u.Password = "";
                };
                usersDictionary.Add("users", userList);
                return Ok(usersDictionary);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add_user")]
        public ActionResult AddUser([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Passed");
                }
                var log = new Log()
                {
                    Account = user.Account,
                    Action = $"{user.Username} added",
                    Role = user.Role
                };
                
                _UCO.AddUser(user);
                _LCO.AddLog(log);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}