namespace block_auth_api.Orchestration.DeviceContract
{
    public interface IDeviceContractOrchestration
    {
        int GetNumDevices();

        void ShouldBeAbleCallAndReadEventLogs();
    }


}