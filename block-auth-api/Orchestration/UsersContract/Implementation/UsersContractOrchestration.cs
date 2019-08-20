using block_auth_api.Connection;
using block_auth_api.Models;
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
    }
}