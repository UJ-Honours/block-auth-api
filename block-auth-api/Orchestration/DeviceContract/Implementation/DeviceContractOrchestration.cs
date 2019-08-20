using block_auth_api.Connection;
using block_auth_api.Models;
using Nethereum.Hex.HexTypes;
using RestSharp;
using System.Collections.Generic;
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

        public void TriggerEvent(string device)
        {
            var accountAddress = _ContractManager.AdminAccount();
            var gas = new HexBigInteger(new BigInteger(400000));
            var value = new HexBigInteger(new BigInteger(0));

            var loginFunction = _ContractManager
                .GetLoginAdminFunction()
                .SendTransactionAsync(accountAddress, gas, value,device);
            loginFunction.Wait();
        }

        public void AddDevice(Device device)
        {
            var accountAddress = _ContractManager.AdminAccount();
            var gas = new HexBigInteger(new BigInteger(400000));
            var value = new HexBigInteger(new BigInteger(0));

            var loginFunction = _ContractManager
                .GetAddDeviceFunction()
                .SendTransactionAsync(accountAddress, gas, value, device.Name, device.Ip, device.Account);
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

        public void TriggerEvent()
        {
            var accountAddress = _ContractManager.AdminAccount();
            var gas = new HexBigInteger(new BigInteger(400000));
            var value = new HexBigInteger(new BigInteger(0));

            var loginFunction = _ContractManager
                .GetLoginAdminFunction()
                .SendTransactionAsync(accountAddress, gas, value);
            loginFunction.Wait();
        }

        public LoggedIn DeviceAuth(string url)
        {
            var requestB = new RestRequest()
            {
                Method = Method.POST,
                Resource = "/auth_data"
            };
            var client = new RestClient(url);
            var responseB = client.Post<LoggedIn>(requestB);

            return responseB.Data;
        }

        public string AccessDevice(LoggedIn loggedIn,string url)
        {
            var requestC = new RestRequest()
            {
                Method = Method.POST,
                Resource = "/connect"
            };
            var client = new RestClient(url);
            requestC.AddParameter("message", $"{loggedIn.Token}");
            var responseC = client.Execute(requestC);

            return responseC.Content;
        }

        public string GetDevice(string url)
        {
            var requestA = new RestRequest()
            {
                Method = Method.GET,
                Resource = "/"
            };
            var client = new RestClient(url);
            var responseA = client.Execute(requestA);
            return responseA.Content;
        }
    }
}