using block_auth_api.Models;

namespace block_auth_api.Orchestration
{
    public interface IOwnerRoleOrchestration
    {
        bool GetOwnerRoleOn();

        bool GetOwnerRoleOff();

        bool UpdateOwnerRole(RolePermission role);

    }
}
