using block_auth_api.Models;
using System.Collections.Generic;

namespace block_auth_api.Orchestration.AccountContract
{
    public interface IAccountContractOrchestration
    {
        BlockAuthAccount GetAccount();

        Dictionary<string, List<BlockAuthAccount>> GetAccounts();

        BlockAuthAccount CreateAccount();
    }
}
