using block_auth_api.Models;
using Nethereum.Contracts;
using Nethereum.Web3;
using Newtonsoft.Json;

namespace block_auth_api.Connection
{
    public class ContractManager : IContractManager
    {
        private readonly Contract _ResourceContract;
        private readonly string _PrivateKey;

        public ContractManager(ResourceContractOptions rco)
        {
            var abi = JsonConvert.SerializeObject(rco.ABI).Replace('"', '\'');
            var contractAddress = rco.Address;
            var endpoint = rco.Endpoint;
            var web3 = new Web3(endpoint);
            _ResourceContract = web3.Eth.GetContract(abi, contractAddress);
            _PrivateKey = rco.privateKey;
        }

        public Contract GetContract()
        {
            return _ResourceContract;
        }

        public string ContractKey() {
            return _PrivateKey;
        }
    }
}