using block_auth_api.Models;
using System.Collections.Generic;

namespace block_auth_api.Orchestration
{
    public interface ITaskOrchestration
    {
        void CreateTask();

        void ToggleTask();

        Dictionary<string, List<Task>> GetTasks();

        int GetTaskCount();

        Task GetTask(int index);
    }
}
