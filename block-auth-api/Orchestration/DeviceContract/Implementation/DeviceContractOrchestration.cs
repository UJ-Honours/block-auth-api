using block_auth_api.Connection;
using Nethereum.Contracts;
using System;
using System.Numerics;

namespace block_auth_api.Orchestration.DeviceContract
{
    public class DeviceContractOrchestration : IDeviceContractOrchestration
    {
        private readonly IContractManager _ContractManager;

        public DeviceContractOrchestration(IContractManager contractManager)
        {
            _ContractManager = contractManager;
        }

        public int GetNumDevices()
        {
            var voteContract = _ContractManager.GetContract();

            var deviceCountFunction = voteContract.GetFunction("userCount").CallAsync<BigInteger>();
            deviceCountFunction.Wait();
            var deviceCount = (int)deviceCountFunction.Result;

            return deviceCount;
        }

        public void ShouldBeAbleCallAndReadEventLogs()
        {
            //var senderAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
            

        }
    }
}