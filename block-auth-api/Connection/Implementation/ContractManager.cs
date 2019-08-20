using block_auth_api.Models;
using Nethereum.Contracts;
using Nethereum.Web3;
using Newtonsoft.Json;

namespace block_auth_api.Connection
{
    public class ContractManager : IContractManager
    {
        private readonly Contract _ResourceContract;
        private readonly string _AdminAccount;

        public ContractManager(ResourceContractOptions rco)
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

        public Function GetDeviceCountFunction()
        {
            return _ResourceContract.GetFunction("deviceCount");
        }

        public Function GetDevicesFunction()
        {
            return _ResourceContract.GetFunction("devices");
        }

        public Function GetDocumentsFunction()
        {
            return _ResourceContract.GetFunction("documents");
        }

        public Function GetStoreDocumentFunction()
        {
            return _ResourceContract.GetFunction("StoreDocument");
        }

        public Function GetAddDeviceFunction()
        {
            return _ResourceContract.GetFunction("addDevice");
        }

        public string AdminAccount() {
            return _AdminAccount;
        }
    }
}