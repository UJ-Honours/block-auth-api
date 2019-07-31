using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Web3.Accounts;

namespace block_auth_api.Orchestration.AccountContract
{
    public class AccountContractOrchestration : IAccountContractOrchestration
    {
        public string CreateAccount()
        {
            var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
            var privateKey = ecKey.GetPrivateKeyAsBytes().ToHex();
            var account = new Account(privateKey);

            return account.Address;
        }

        public string GetAccount()
        {
            throw new System.NotImplementedException();
        }
    }
}
