using block_auth_api.Models;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Newtonsoft.Json;
using System.Numerics;

namespace block_auth_api.Connection
{
    public class UserContractManager : IUserContractManager
    {
        private readonly Contract _ResourceContract;
        private readonly string _AdminAccount;

        public UserContractManager(ResourceContractOptions rco)
        {
            var abi = JsonConvert.SerializeObject(rco.ABI).Replace('"', '\'');
            var contractAddress = rco.Address;
            var endpoint = rco.Endpoint;
            var web3 = new Web3(endpoint);
            _ResourceContract = web3.Eth.GetContract(abi, contractAddress);
            _AdminAccount = rco.AdminAccount;
        }


        public Function GetAddUserFunction()
        {
            return _ResourceContract.GetFunction("addUser");
        }

        public Function GetUsersFunction()
        {
            return _ResourceContract.GetFunction("users");
        }

        public Function GetUserCountFunction()
        {
            return _ResourceContract.GetFunction("userCount");
        }

        public string AdminAccount()
        {
            return _AdminAccount;
        }

        public HexBigInteger GetGasAmount()
        {
            return new HexBigInteger(new BigInteger(400000));
        }

        public HexBigInteger GetValueAmount()
        {
            return new HexBigInteger(new BigInteger(0));
        }
    }
}