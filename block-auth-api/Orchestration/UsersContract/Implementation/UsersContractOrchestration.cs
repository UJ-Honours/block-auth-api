using block_auth_api.Connection;
using block_auth_api.Models;
using Nethereum.Hex.HexTypes;
using System.Collections.Generic;
using System.Numerics;

namespace block_auth_api.Orchestration.UsersContract
{
    public class UsersContractOrchestration : IUsersContractOrchestration
    {
        private readonly IContractManager _ContractManager;

        public UsersContractOrchestration(IContractManager contractManager)
        {
            _ContractManager = contractManager;
        }

        public void AddUser(User user)
        {
            var accountAddress = _ContractManager.AdminAccount();
            var gas = new HexBigInteger(new BigInteger(400000));
            var value = new HexBigInteger(new BigInteger(0));

            var addUserFunction = _ContractManager
                .GetAddUserFunction()
                .SendTransactionAsync(accountAddress, gas, value, user.Name, user.Account);
            addUserFunction.Wait();
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