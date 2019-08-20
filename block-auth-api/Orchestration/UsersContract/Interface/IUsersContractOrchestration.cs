using block_auth_api.Models;

namespace block_auth_api.Orchestration.UsersContract
{
    public interface IUsersContractOrchestration
    {
        int GetUserCount();

        User GetUser(int index);
    }
}