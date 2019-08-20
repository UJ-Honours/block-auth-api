using block_auth_api.Connection;
using block_auth_api.Models;
using block_auth_api.Orchestration.UsersContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Hex.HexTypes;
using System.Collections.Generic;
using System.Numerics;

namespace block_auth_api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]"),Authorize]
    public class UsersController : Controller
    {
        private readonly IUsersContractOrchestration _UCO;
        private readonly IContractManager _ContractManager;

        public UsersController(IContractManager contractManager, IUsersContractOrchestration uco)
        {
            _ContractManager = contractManager;
            _UCO = uco;
        }

        [HttpGet]
        [Route("users")]
        public ActionResult GetUsers()
        {
            var userList = new List<User>();

            var userCount = _UCO.GetUserCount();

            for (int i = 0; i < userCount; i++)
            {
                var user = _UCO.GetUser(i);
                userList.Add(user);
            }

            return Ok(userList);
        }

        [HttpPost]
        [Route("add_user")]
        public ActionResult AddUser([FromBody] User user)
        {
            var accountAddress = _ContractManager.AdminAccount();
            var gas = new HexBigInteger(new BigInteger(400000));
            var value = new HexBigInteger(new BigInteger(0));

            var addUserFunction = _ContractManager
                .GetAddUserFunction()
                .SendTransactionAsync(accountAddress, gas, value, user.Name, user.Account);
            addUserFunction.Wait();

            return Ok(user);
        }

    }
}
