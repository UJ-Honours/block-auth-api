using block_auth_api.Connection;
using block_auth_api.Models;
using System.Collections.Generic;
using System.Numerics;

namespace block_auth_api.Orchestration
{
    public class TaskOrchestration : ITaskOrchestration
    {
        private readonly ITaskContractManager _ContractManager;

        public TaskOrchestration(ITaskContractManager contractManager)
        {
            _ContractManager = contractManager;
        }

        public void CreateTask()
        {
            var accountAddress = _ContractManager.AdminAccount();
            var gas = _ContractManager.GetGasAmount();
            var value = _ContractManager.GetValueAmount();

            var loginFunction = _ContractManager
                .GetCreateTaskFunction()
                .SendTransactionAsync(accountAddress, gas, value, "task 1");
            loginFunction.Wait();
        }

        public Dictionary<string, List<Task>> GetTasks()
        {
            var deviceDictionary = new Dictionary<string, List<Task>>();
            var deviceList = new List<Task>();

            var deviceCount = GetTaskCount();

            for (int i = 0; i < deviceCount; i++)
            {
                var device = GetTask(i);
                deviceList.Add(device);
            }

            deviceDictionary.Add("devices", deviceList);
            return deviceDictionary;
        }

        public void ToggleTask()
        {
            var accountAddress = _ContractManager.AdminAccount();
            var gas = _ContractManager.GetGasAmount();
            var value = _ContractManager.GetValueAmount();

            var toggleTaskFunction = _ContractManager
                .GetToggleTaskFunction()
                .SendTransactionAsync(accountAddress, gas, value, 3);
            toggleTaskFunction.Wait();
        }
        public int GetTaskCount()
        {
            var taskCountFunction = _ContractManager
                .GetTaskCountFunction()
                .CallAsync<BigInteger>();
            taskCountFunction.Wait();
            var taskCount = (int)taskCountFunction.Result;
            return taskCount;
        }

        public Task GetTask(int index)
        {
            var result = _ContractManager
                    .GetTasksFunction()
                    .CallDeserializingToObjectAsync<Task>(index);
            result.Wait();
            return result.Result;
        }
    }
}
