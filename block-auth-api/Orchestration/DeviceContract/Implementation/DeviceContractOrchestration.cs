using block_auth_api.Connection;
using block_auth_api.Models;
using block_auth_api.Orchestration.AccountContract;
using RestSharp;
using System.Collections.Generic;
using System.Numerics;

namespace block_auth_api.Orchestration.DeviceContract
{
    public class DeviceContractOrchestration : IDeviceContractOrchestration
    {
        private readonly IDeviceContractManager _ContractManager;
        private readonly IAccountContractOrchestration _ACO;

        public DeviceContractOrchestration(IDeviceContractManager contractManager, IAccountContractOrchestration aco)
        {
            _ContractManager = contractManager;
            _ACO = aco;
        }

        public Device GetDevice(int index)
        {
            var result = _ContractManager
                    .GetDevicesFunction()
                    .CallDeserializingToObjectAsync<Device>(1, index);
            result.Wait();
            return result.Result;
        }

        public int GetDeviceCount()
        {
            var deviceCountFunction = _ContractManager
                .GetDeviceCountFunction()
                .CallAsync<BigInteger>();
            deviceCountFunction.Wait();
            var deviceCount = (int)deviceCountFunction.Result;
            return deviceCount;
        }

        public void AddDevice(Device device)
        {
            var accountAddress = _ContractManager.AdminAccount();
            var gas = _ContractManager.GetGasAmount();
            var value = _ContractManager.GetValueAmount();

            var newAccount = _ACO.CreateAccount().Address;

            var loginFunction = _ContractManager
                .GetAddDeviceFunction()
                .SendTransactionAsync(accountAddress, gas, value, device.Name, device.Ip);
            loginFunction.Wait();
        }

        public Dictionary<string, List<Device>> GetDevices()
        {
            var deviceDictionary = new Dictionary<string, List<Device>>();
            var deviceList = new List<Device>();

            var deviceCount = GetDeviceCount();

            for (int i = 0; i < deviceCount; i++)
            {
                var device = GetDevice(i);
                deviceList.Add(device);
            }

            deviceDictionary.Add("devices", deviceList);
            return deviceDictionary;
        }

        public void TriggerEvent(string account)
        {
            var accountAddress = account;
            var gas = _ContractManager.GetGasAmount();
            var value = _ContractManager.GetValueAmount();

            var loginFunction = _ContractManager
                .GetLoginAdminFunction()
                .SendTransactionAsync(accountAddress, gas, value);
            loginFunction.Wait();
        }

        public LoggedIn DeviceAuth(string url)
        {
            var request = new RestRequest()
            {
                Method = Method.POST,
                Resource = "/auth_data"
            };
            var client = new RestClient(url);
            var response = client.Post<LoggedIn>(request);

            return response.Data;
        }

        public string AccessDevice(LoggedIn loggedIn, string url)
        {
            var request = new RestRequest()
            {
                Method = Method.POST,
                Resource = "/connect"
            };
            var client = new RestClient(url);
            request.AddParameter("message", $"{loggedIn.Token}");
            var response = client.Execute(request);

            return response.Content;
        }

        public string GetDevice(string url)
        {
            var request = new RestRequest()
            {
                Method = Method.GET,
                Resource = "/"
            };
            var client = new RestClient(url);
            var response = client.Execute(request);
            return response.Content;
        }
    }
}