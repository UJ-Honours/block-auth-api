using Nethereum.Contracts;

namespace block_auth_api.Connection
{
    public interface IContractManager
    {
        Contract GetContract();
    }
}