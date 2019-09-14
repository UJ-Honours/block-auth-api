using block_auth_api.Models;
using System.Collections.Generic;

namespace block_auth_api.Orchestration.UsersContract
{
    public interface IUsersContractOrchestration
    {
        int GetUserCount();

        User GetUser(int index);

        List<User> GetUsers();

        void AddUser(User user);
    }
}