using block_auth_api.Connection;
using block_auth_api.Models;
using block_auth_api.Orchestration.UsersContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace block_auth_api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]"), Authorize]
    public class UsersController : Controller
    {
        private readonly IUsersContractOrchestration _UCO;

        public UsersController(IUsersContractOrchestration uco)
        {
            _UCO = uco;
        }

        [HttpGet]
        [Route("users")]
        public ActionResult GetUsers()
        {
            try
            {
                var userList = _UCO.GetUsers();

                return Ok(userList);
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
            _UCO.AddUser(user);

            return Ok(user);
        }
    }
}