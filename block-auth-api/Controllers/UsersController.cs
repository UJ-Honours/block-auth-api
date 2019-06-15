using block_auth_api.Models;
using block_auth_api.Orchestration.UsersContract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace block_auth_api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersContractOrchestration _UCO;

        public UsersController(IUsersContractOrchestration uco)
        {
            _UCO = uco;
        }

        [HttpGet]
        public ActionResult<int> GetNumDevices()
        {
            return _UCO.GetNumUsers();
        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return new List<User>();
        }

        [HttpPost]
        public ActionResult<User> AddUser()
        {
            return new User { };
        }

        [HttpDelete]
        public ActionResult<User> DeleteUser()
        {
            return new User { };
        }

        [HttpPut]
        public ActionResult<User> UpdateUser()
        {
            return new User { };
        }
    }
}