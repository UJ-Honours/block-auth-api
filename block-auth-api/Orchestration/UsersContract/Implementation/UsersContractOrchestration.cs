using block_auth_api.Connection;
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

        public int GetNumUsers()
        {
            var loginContract = _ContractManager.GetContract();

            var userCountFunction = loginContract.GetFunction("deviceCount").CallAsync<BigInteger>();
            userCountFunction.Wait();
            var userCount = (int)userCountFunction.Result;

            return userCount;
        }
    }
}