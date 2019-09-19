using block_auth_api.Models;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Newtonsoft.Json;
using System.Numerics;

namespace block_auth_api.Connection
{
    public class RoleContractManager : IRoleContractManager
    {
        private readonly Contract _ResourceContract;
        private readonly string _AdminAccount;

        public RoleContractManager(RoleContractOptions dco)
        {
            var abi = JsonConvert.SerializeObject(dco.ABI).Replace('"', '\'');
            var contractAddress = dco.Address;
            var endpoint = dco.Endpoint;
            var web3 = new Web3(endpoint);
            _ResourceContract = web3.Eth.GetContract(abi, contractAddress);
            _AdminAccount = dco.AdminAccount;
        }

        public Function GetOwnerRoleFunction()
        {
            return _ResourceContract.GetFunction("or");
        }

        public Function GetGuestRoleFunction()
        {
            return _ResourceContract.GetFunction("gr");
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

        public Function GetUpdateOwnerRoleFunction()
        {
            return _ResourceContract.GetFunction("UpdateOwnerRole");
        }

        public Function GetUpdateGuestRoleFunction()
        {
            return _ResourceContract.GetFunction("UpdateGuestRole");
        }
    }
}
