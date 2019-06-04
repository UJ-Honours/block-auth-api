using block_auth_api.Connection;
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
            var numDevices = _ContractManager.GetContract().GetFunction("userCount").CallAsync<BigInteger>();
            numDevices.Wait();

            return 1;
        }
    }
}