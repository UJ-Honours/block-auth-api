using block_auth_api.Models;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Newtonsoft.Json;
using System.Numerics;

namespace block_auth_api.Connection
{
    public class TaskContractManager : ITaskContractManager
    {
        private readonly Contract _ResourceContract;
        private readonly string _AdminAccount;
        public TaskContractManager(TaskContractOptions tco)
        {
            var abi = JsonConvert.SerializeObject(tco.ABI).Replace('"', '\'');
            var contractAddress = tco.Address;
            var endpoint = tco.Endpoint;
            var web3 = new Web3(endpoint);
            _ResourceContract = web3.Eth.GetContract(abi, contractAddress);
            _AdminAccount = tco.AdminAccount;
        }

        public Function GetCreateTaskFunction()
        {
            return _ResourceContract.GetFunction("createTask");
        }

        public Function GetTaskCountFunction()
        {
            return _ResourceContract.GetFunction("taskCount");
        }

        public Function GetTasksFunction()
        {
            return _ResourceContract.GetFunction("tasks");
        }

        public Function GetToggleTaskFunction()
        {
            return _ResourceContract.GetFunction("toggleCompleted");
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
