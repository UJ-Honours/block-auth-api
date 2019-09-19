using block_auth_api.Models;

namespace block_auth_api.Orchestration
{
    public interface IRoleOrchestration
    {
        Role GetOwnerRole();

        Role GetGuestRole();

        bool UpdateOwnerRole(Role role);

        bool UpdateGuestRole(Role role);
    }
}
