using block_auth_api.Models;

namespace block_auth_api.Orchestration.TokenOrchestration
{
    public interface ITokenOrchestration
    {
        string BuildToken(User user);

        User Authenticate(User user);
    }
}