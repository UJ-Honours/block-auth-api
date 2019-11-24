using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;

namespace block_auth_api.Connection
{
    public interface IOwnerRoleContractManager
    {
        Function GetOnFunction();

        Function GetOffFunction();

        Function GetUpdateOwnerRoleFunction();

        string AdminAccount();

        HexBigInteger GetGasAmount();

        HexBigInteger GetValueAmount();
    }
}
