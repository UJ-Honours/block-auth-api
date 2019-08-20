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
            var keyStoreEncryptedJson =
                        @"{""crypto"":{""cipher"":""aes-128-ctr"",""ciphertext"":""b4f42e48903879b16239cd5508bc5278e5d3e02307deccbec25b3f5638b85f91"",""cipherparams"":{""iv"":""dc3f37d304047997aa4ef85f044feb45""},""kdf"":""scrypt"",""mac"":""ada930e08702b89c852759bac80533bd71fc4c1ef502291e802232b74bd0081a"",""kdfparams"":{""n"":65536,""r"":1,""p"":8,""dklen"":32,""salt"":""2c39648840b3a59903352b20386f8c41d5146ab88627eaed7c0f2cc8d5d95bd4""}},""id"":""19883438-6d67-4ab8-84b9-76a846ce544b"",""address"":""12890d2cce102216644c59dae5baed380d84830c"",""version"":3}";
            var account = Account.LoadFromKeyStore(keyStoreEncryptedJson, password);
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
