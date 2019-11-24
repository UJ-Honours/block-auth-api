using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;

namespace block_auth_api.Connection
{
    public interface IGuestRoleContractManager
    {
        Function GetOnFunction();

        Function GetOffFunction();

        Function GetUpdateGuestRoleFunction();

        string AdminAccount();

        HexBigInteger GetGasAmount();

        HexBigInteger GetValueAmount();
    }
}
