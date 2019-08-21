using block_auth_api.Models;

namespace block_auth_api.Orchestration.TokenOrchestration
{
    public interface ITokenOrchestration
    {
        string BuildToken(UserVM user);

        User Authenticate(UserVM login);
    }
}
