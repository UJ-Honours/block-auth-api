using block_auth_api.Connection;
using block_auth_api.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Hex.HexTypes;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;

namespace block_auth_api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IContractManager _ContractManager;

        public UsersController(IContractManager contractManager)
        {
            _ContractManager = contractManager;
        }

        [HttpGet]
        [Route("users")]
        public ActionResult GetUsers()
        {
            string sqlIoTUserDetails = "SELECT * FROM IoTUser;";
            var iotUsersDic = new Dictionary<string, List<User>>();
            using (var connection = new SqlConnection(_ContractManager.GetConnectionString()))
            {
                var iotUsers = connection.Query<User>(sqlIoTUserDetails).ToList();
                iotUsersDic.Add("users", iotUsers);
                return Ok(iotUsersDic);
            }
        }

        [HttpPost]
        [Route("users")]
        public ActionResult AddUser([FromBody] User user)
        {
            var accountAddress = _ContractManager.AdminAccount();
            var gas = new HexBigInteger(new BigInteger(400000));
            var value = new HexBigInteger(new BigInteger(0));

            var addUserFunction = _ContractManager
                .GetAddUserFunction()
                .SendTransactionAsync(accountAddress, gas, value, user.Name, user.Account);
            addUserFunction.Wait();

            string iotUserInsert = "INSERT INTO IoTUser (Name,Account) Values (@Name,@Account);";

            using (var connection = new SqlConnection(_ContractManager.GetConnectionString()))
            {
                var affectedRows = connection.Execute(iotUserInsert, new { user.Name, user.Account });
            }
            return Ok(user);
        }

    }
}
