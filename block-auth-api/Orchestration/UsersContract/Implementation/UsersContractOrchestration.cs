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
            var numUsers = _ContractManager.GetContract().GetFunction("userCount").CallAsync<BigInteger>();
            numUsers.Wait();

            return 1;
        }
    }
}