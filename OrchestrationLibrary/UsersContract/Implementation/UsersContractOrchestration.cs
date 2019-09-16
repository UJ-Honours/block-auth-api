using block_auth_api.Connection;
using block_auth_api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace block_auth_api.Orchestration
{
    public class UsersContractOrchestration : IUsersContractOrchestration
    {
        private readonly IUserContractManager _ContractManager;

        public UsersContractOrchestration(IUserContractManager contractManager)
        {
            _ContractManager = contractManager;
        }

        public void AddUser(User user)
        {
            var newAccount = "0x4cc7d1aee9c76466b0787939c6355281de01a111";
            user.Account = newAccount;
            //user.Role = "guest";

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
                    .CallDeserializingToObjectAsync<User>(index);
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

            for (int i = 1; i <= userCount; i++)
            {
                var user = GetUser(i);
                userList.Add(user);
            }
            return userList;
        }
    }
}