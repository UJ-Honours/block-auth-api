using block_auth_api.Orchestration.AccountContract;
using Microsoft.AspNetCore.Mvc;

namespace block_auth_api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountsController:Controller
    {
        private readonly IAccountContractOrchestration _ACO;

        public AccountsController(IAccountContractOrchestration aco)
        {
            _ACO = aco;
        }

        [HttpGet]
        [Route("account")]
        public ActionResult GetAccount()
        {
            var account = _ACO.GetAccount();
            return Ok(account);
        }

        [HttpPost]
        [Route("account")]
        public ActionResult CreateAccount()
        {
            var account = _ACO.CreateAccount();
            return Ok(account);
        }

        
    }
}
