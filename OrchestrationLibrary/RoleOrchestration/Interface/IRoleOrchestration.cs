using block_auth_api.Models;
using System.Collections.Generic;

namespace block_auth_api.Orchestration
{
    public interface IRoleOrchestration
    {
        void CreateRole(string name);

        int GetRoleCount();

        Role GetRole(int index);

        Dictionary<string, List<Role>> GetRoles();
    }
}
