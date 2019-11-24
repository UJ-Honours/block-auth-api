using block_auth_api.Models;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Newtonsoft.Json;
using System.Numerics;

namespace block_auth_api.Connection
{
    public class GuestRoleContractManager : IGuestRoleContractManager
    {
        private readonly Contract _ResourceContract;
        private readonly string _AdminAccount;

        public GuestRoleContractManager(GuestRoleContractOptions dco)
        {
            var abi = JsonConvert.SerializeObject(dco.ABI).Replace('"', '\'');
            var contractAddress = dco.Address;
            var endpoint = dco.Endpoint;
            var web3 = new Web3(endpoint);
            _ResourceContract = web3.Eth.GetContract(abi, contractAddress);
            _AdminAccount = dco.AdminAccount;
        }

        public Function GetOnFunction()
        {
            return _ResourceContract.GetFunction("on");
        }

        public Function GetOffFunction()
        {
            return _ResourceContract.GetFunction("off");
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

        public Function GetUpdateGuestRoleFunction()
        {
            return _ResourceContract.GetFunction("updateGuestRole");
        }


    }
}
