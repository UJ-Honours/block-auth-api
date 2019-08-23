using block_auth_api.Models;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using Nethereum.Web3.Accounts;
using System.Collections.Generic;

namespace block_auth_api.Orchestration.AccountContract
{
    public class AccountContractOrchestration : IAccountContractOrchestration
    {
        public BlockAuthAccount CreateAccount()
        {
            var ecKey = EthECKey.GenerateKey();
            var privateKey = ecKey.GetPrivateKeyAsBytes().ToHex();
            var getAccount = new Account(privateKey);
            var account = new BlockAuthAccount()
            {
                Address = getAccount.Address,
                PrivateKey = getAccount.PrivateKey
            };
            return account;
        }

        public BlockAuthAccount GetAccount()
        {
            var password = "password";
            var accountFilePath = @"c:\xxx\UTC--2015-11-25T05-05-03.116905600Z--12890d2cce102216644c59dae5baed380d84830c";
            var account = Account.LoadFromKeyStore(accountFilePath, password);
            var bAccount = new BlockAuthAccount()
            {
                Address = account.Address,
                PrivateKey = account.PrivateKey
            };
            return bAccount;
        }

        public Dictionary<string, List<BlockAuthAccount>> GetAccounts()
        {
            var accountsDictionary = new Dictionary<string, List<BlockAuthAccount>>();

            return accountsDictionary;
        }
    }
}