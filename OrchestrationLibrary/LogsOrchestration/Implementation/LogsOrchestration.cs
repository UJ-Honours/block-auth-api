using block_auth_api.Connection;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace OrchestrationLibrary.LogsOrchestration
{
    public class LogsOrchestration : ILogsOrchestration
    {
        private readonly ILogContractManager _ContractManager;

        public LogsOrchestration(ILogContractManager contract)
        {
            _ContractManager = contract;
        }

        public Log GetLog(int index)
        {
            var result = _ContractManager
                    .GetLogsFunction()
                    .CallDeserializingToObjectAsync<Log>(index);

            result.Wait();

            return result.Result;
        }

        public bool AddLog(Log log)
        {
            try
            {
                var accountAddress = _ContractManager.AdminAccount();
                var gas = _ContractManager.GetGasAmount();
                var value = _ContractManager.GetValueAmount();

                var loginFunction = _ContractManager
                    .GetAddLogFunction()
                    .SendTransactionAsync(accountAddress, gas, value, log.Action, log.Account, log.Role);

                loginFunction.Wait();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetLogsCount()
        {
            var logCountFunction = _ContractManager
                .GetLogsCountFunction()
                .CallAsync<BigInteger>();

            logCountFunction.Wait();

            var logsCount = (int)logCountFunction.Result;

            return logsCount;
        }

        public Dictionary<string, List<Log>> GetLogs()
        {
            try
            {
                var deviceList = new List<Log>();
                var size = GetLogsCount();
                if (size > 0)
                {

                    for (int i = 1; i <= size; i++)
                    {

                        var device = GetLog(i);
                        deviceList.Add(device);
                    }

                    var deviceDictionary = new Dictionary<string, List<Log>>
                    {
                        { "logs", deviceList }
                    };

                    return deviceDictionary;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return new Dictionary<string, List<Log>>();
            }
        }

    }
}
