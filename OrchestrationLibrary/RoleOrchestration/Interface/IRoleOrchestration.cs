using block_auth_api.Models;

namespace block_auth_api.Orchestration
{
    public interface IRoleOrchestration
    {
        RolePermission GetOwnerRole();

        RolePermission GetGuestRole();

        bool UpdateOwnerRole(RolePermission role);

        bool UpdateGuestRole(RolePermission role);
    }
}
