using block_auth_api.Models;
using System.Collections.Generic;

namespace block_auth_api.Orchestration
{
    public interface IDeviceContractOrchestration
    {
        int GetDeviceCount();

        Device GetDevice(int index);

        Dictionary<string, List<Device>> GetDevices();

        void TriggerEvent(LoggedIn loggedIn);

        bool AddDevice(Device device);

        void RemoveDevice(Device device);

        LoggedIn DeviceAuth(LoggedIn loggedIn);

        string AccessDevice(LoggedIn loggedIn);

        bool GetDevice(string url);

        string TurnDeviceOn(Device device);

        string TurnDeviceOff(Device device);
    }
}