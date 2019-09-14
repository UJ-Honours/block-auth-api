using block_auth_api.Models;
using System.Collections.Generic;

namespace block_auth_api.Orchestration.DeviceContract
{
    public interface IDeviceContractOrchestration
    {
        int GetDeviceCount();

        Device GetDevice(int index);

        Dictionary<string, List<Device>> GetDevices();

        void TriggerEvent(LoggedIn loggedIn);

        void AddDevice(Device device);

        void RemoveDevice(Device device);

        LoggedIn DeviceAuth(LoggedIn loggedIn);

        string AccessDevice(LoggedIn loggedIn);

        string GetDevice(string url);
    }
}