using block_auth_api.Models;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Newtonsoft.Json;
using System.Numerics;

namespace block_auth_api.Connection
{
    public class DeviceContractManager : IDeviceContractManager
    {
        private readonly Contract _ResourceContract;
        private readonly string _AdminAccount;

        public DeviceContractManager(ResourceContractOptions rco)
        {
            var abi = JsonConvert.SerializeObject(rco.ABI).Replace('"', '\'');
            var contractAddress = rco.Address;
            var endpoint = rco.Endpoint;
            var web3 = new Web3(endpoint);
            _ResourceContract = web3.Eth.GetContract(abi, contractAddress);
            _AdminAccount = rco.AdminAccount;
        }

        public Function GetLoginAdminFunction()
        {
            return _ResourceContract.GetFunction("login_admin");
        }

       
        public Function GetDeviceCountFunction()
        {
            return _ResourceContract.GetFunction("deviceCount");
        }

        public Function GetDevicesFunction()
        {
            return _ResourceContract.GetFunction("devices");
        }

        public Function GetAddDeviceFunction()
        {
            return _ResourceContract.GetFunction("addDevice");
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