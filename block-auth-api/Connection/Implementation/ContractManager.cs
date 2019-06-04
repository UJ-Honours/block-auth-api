using block_auth_api.Models;
using Microsoft.Extensions.Configuration;
using Nethereum.Contracts;
using Nethereum.Web3;
using Newtonsoft.Json;

namespace block_auth_api.Connection
{
    public class ContractManager : IContractManager
    {
        private readonly Contract _ResourceContract;

        public ContractManager(ResourceContractOptions rco)
        {
            //var abi = JsonConvert.SerializeObject(rco.ABI).Replace('"', '\'');

            var abi = @"[{'constant':true,'inputs':[],'name':'candidate1','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'candidate','type':'uint256'}],'name':'castVote','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'candidate2','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'name':'','type':'address'}],'name':'voted','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'}]";
            var contractAddress = rco.ContractAddress;
            var endpoint = rco.Endpoint;

            //Creates the connecto to the network and gets an instance of the contract.
            var web3 = new Web3(endpoint);
            _ResourceContract = web3.Eth.GetContract(abi, contractAddress);
        }

        public Contract GetContract()
        {
            return _ResourceContract;
        }
    }
}