using block_auth_api.Models;
using System.Collections.Generic;

namespace block_auth_api.Orchestration.DeviceContract
{
    public interface IDeviceContractOrchestration
    {
        int GetDeviceCount();

        Device GetDevice(int index);

        Dictionary<string, List<Device>> GetDevices();

        void TriggerEvent();

        void AddDevice(Device device);

        LoggedIn DeviceAuth(string url);

        string AccessDevice(LoggedIn loggedIn,string url);

        string GetDevice(string url);
    }
}