using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;

namespace block_auth_api.Connection
{
    public interface IRoleContractManager
    {
        Function GetOwnerRoleFunction();

        Function GetGuestRoleFunction();

        Function GetUpdateOwnerRoleFunction();

        Function GetUpdateGuestRoleFunction();

        string AdminAccount();

        HexBigInteger GetGasAmount();

        HexBigInteger GetValueAmount();
    }
}
