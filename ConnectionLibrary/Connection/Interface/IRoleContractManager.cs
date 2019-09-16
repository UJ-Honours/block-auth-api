using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;

namespace block_auth_api.Connection
{
    public interface IRoleContractManager
    {
        Function GetCreateRoleFunction();

        Function GetRolesFunction();

        Function GetRoleCountFunction();

        string AdminAccount();

        HexBigInteger GetGasAmount();

        HexBigInteger GetValueAmount();
    }
}
