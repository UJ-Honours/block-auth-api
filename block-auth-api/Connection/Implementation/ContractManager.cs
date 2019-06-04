using block_auth_api.Models;
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
            var abi = JsonConvert.SerializeObject(rco.ABI).Replace('"', '\'');
            var contractAddress = rco.Address;
            var endpoint = rco.Endpoint;
            //var abi = @"[{'constant':true,'inputs':[],'name':'userCount','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function','Signature':'0x07973ccf'},{'constant':true,'inputs':[],'name':'hash','outputs':[{'name':'','type':'bytes32'}],'payable':false,'stateMutability':'view','type':'function','Signature':'0x09bd5a60'},{'constant':true,'inputs':[{'name':'','type':'uint256'}],'name':'devices','outputs':[{'name':'id','type':'uint256'},{'name':'content','type':'string'},{'name':'completed','type':'bool'}],'payable':false,'stateMutability':'view','type':'function','Signature':'0x10ff8e31'},{'constant':true,'inputs':[{'name':'','type':'uint256'}],'name':'users','outputs':[{'name':'id','type':'uint256'},{'name':'isAdmin','type':'bool'}],'payable':false,'stateMutability':'view','type':'function','Signature':'0x365b98b2'},{'constant':true,'inputs':[],'name':'deviceCount','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function','Signature':'0x535bca9a'},{'constant':false,'inputs':[{'name':'sender','type':'address'},{'name':'token','type':'bytes32'}],'name':'LoginAttempt','outputs':[],'payable':false,'stateMutability':null,'type':'event','Signature':'0x5913a2a1da9e820000b253b31744f5c4b17e184c6d3f19d8e9beaef994d804f8'},{'constant':false,'inputs':[{'name':'id','type':'uint256'},{'name':'ip','type':'string'},{'name':'added','type':'bool'}],'name':'AddDevice','outputs':[],'payable':false,'stateMutability':null,'type':'event','Signature':'0x62ef04e8205af6be935fdc710300c195cf64de24309c1ca2b704b08b5d90193b'},{'constant':false,'inputs':[{'name':'id','type':'uint256'},{'name':'isAdmin','type':'bool'}],'name':'AddUser','outputs':[],'payable':false,'stateMutability':null,'type':'event','Signature':'0xb880ea1def36322e62e5be5ee25e4d8a77edbac176d64039dfcfb97f145b7f9c'},{'constant':false,'inputs':[],'name':'Login2','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function','Signature':'0x384e8e91'},{'constant':false,'inputs':[{'name':'ip','type':'string'}],'name':'addDevice','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function','Signature':'0xc7eeb96d'},{'constant':false,'inputs':[],'name':'addUsers','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function','Signature':'0x3ae8038e'},{'constant':false,'inputs':[],'name':'rand','outputs':[{'name':'','type':'bytes32'}],'payable':false,'stateMutability':'nonpayable','type':'function','Signature':'0x3b3dca76'},{'constant':false,'inputs':[],'name':'login_admin','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function','Signature':'0xf262c49f'}]";
            var web3 = new Web3(endpoint);
            _ResourceContract = web3.Eth.GetContract(abi, contractAddress);
        }

        public Contract GetContract()
        {
            return _ResourceContract;
        }
    }
}