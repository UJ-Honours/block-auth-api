using block_auth_api.Models;

namespace block_auth_api.Orchestration
{
    public interface IGuestRoleOrchestration
    {
        bool GetGuestRoleOn();

        bool GetGuestRoleOff();

        bool UpdateGuestRole(RolePermission role);

    }
}
