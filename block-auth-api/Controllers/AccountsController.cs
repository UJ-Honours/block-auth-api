using block_auth_api.Orchestration.AccountContract;
using Microsoft.AspNetCore.Mvc;
using System;

namespace block_auth_api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountsController : Controller
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
            try
            {
                var account = _ACO.GetAccount();
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("account")]
        public ActionResult CreateAccount()
        {
            try
            {
                var account = _ACO.CreateAccount();
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}