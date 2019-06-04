using block_auth_api.Orchestration.UsersContract;
using Microsoft.AspNetCore.Mvc;

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
    }
}