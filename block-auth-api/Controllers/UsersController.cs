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

        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "user1" };
        //}

        //[HttpGet]
        //[Route("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return $"user{id}";
        //}

        [HttpGet]
        public ActionResult<int> GetNumDevices()
        {
            return _UCO.GetNumUsers();
        }
    }
}