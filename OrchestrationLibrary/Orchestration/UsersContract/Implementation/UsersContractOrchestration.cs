using block_auth_api.Connection;
using block_auth_api.Models;
using block_auth_api.Orchestration.AccountContract;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace block_auth_api.Orchestration.UsersContract
{
    public class UsersContractOrchestration : IUsersContractOrchestration
    {
        private readonly IUserContractManager _ContractManager;
        private readonly IAccountContractOrchestration _ACO;

        public UsersContractOrchestration(IUserContractManager contractManager, IAccountContractOrchestration aco)
        {
            _ContractManager = contractManager;
            _ACO = aco;
        }

        public void AddUser(User user)
        {
            var newAccount = _ACO.CreateAccount();
            user.Account = newAccount.Address;
            user.Role = "user";

            var users = GetUsers();

            var userResult = users.FirstOrDefault(x => x.Username == user.Username);

            if (userResult == null)
            {
                var accountAddress = _ContractManager.AdminAccount();
                var gas = _ContractManager.GetGasAmount();
                var value = _ContractManager.GetValueAmount();

                var addUserFunction = _ContractManager
                    .GetAddUserFunction()
                    .SendTransactionAsync(accountAddress, gas, value, user.Username, user.Account, user.Password, user.Role);
                addUserFunction.Wait();
            }
        }

        public User GetUser(int index)
        {
            var result = _ContractManager
                    .GetUsersFunction()
                    .CallDeserializingToObjectAsync<User>(1, index);
            result.Wait();
            return result.Result;
        }

        public int GetUserCount()
        {
            var userCountFunction = _ContractManager
                .GetUserCountFunction()
                .CallAsync<BigInteger>();
            userCountFunction.Wait();
            var userCount = (int)userCountFunction.Result;
            return userCount;
        }

        public List<User> GetUsers()
        {
            var userList = new List<User>();

            var userCount = GetUserCount();

            for (int i = 0; i < userCount; i++)
            {
                var user = GetUser(i);
                userList.Add(user);
            }
            return userList;
        }
    }
}