using ModelsLibrary.Models;
using System.Collections.Generic;

namespace OrchestrationLibrary.LogsOrchestration
{
    public interface ILogsOrchestration
    {
        bool AddLog(Log log);

        int GetLogsCount();

        Log GetLog(int index);

        Dictionary<string, List<Log>> GetLogs();
    }
}
