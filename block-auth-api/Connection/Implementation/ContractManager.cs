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
        private readonly string _ConnectionString;

        public ContractManager(ResourceContractOptions rco)
        {
            var abi = JsonConvert.SerializeObject(rco.ABI).Replace('"', '\'');
            var contractAddress = rco.Address;
            var endpoint = rco.Endpoint;
            var web3 = new Web3(endpoint);
            _ResourceContract = web3.Eth.GetContract(abi, contractAddress);
            _AdminAccount = rco.AdminAccount;
            _ConnectionString = rco.ConnectionString;
        }

        public Contract GetContract()
        {
            return _ResourceContract;
        }

        public string AdminAccount() {
            return _AdminAccount;
        }

        public string GetConnectionString()
        {
            return _ConnectionString;
        }
    }
}