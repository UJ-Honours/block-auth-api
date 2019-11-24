using block_auth_api.Models;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Newtonsoft.Json;
using System.Numerics;

namespace block_auth_api.Connection
{
    public class LogContractManager : ILogContractManager
    {
        private readonly Contract _ResourceContract;
        private readonly string _AdminAccount;

        public LogContractManager(LogContractOptions dco)
        {
            var abi = JsonConvert.SerializeObject(dco.ABI).Replace('"', '\'');
            var contractAddress = dco.Address;
            var endpoint = dco.Endpoint;
            var web3 = new Web3(endpoint);
            _ResourceContract = web3.Eth.GetContract(abi, contractAddress);
            _AdminAccount = dco.AdminAccount;
        }


        public Function GetLogsCountFunction()
        {
            return _ResourceContract.GetFunction("logsCount");
        }

        public Function GetLogsFunction()
        {
            return _ResourceContract.GetFunction("logs");
        }

        public Function GetAddLogFunction()
        {
            return _ResourceContract.GetFunction("addLog");
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