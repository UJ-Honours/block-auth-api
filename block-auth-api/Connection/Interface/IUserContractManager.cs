using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;

namespace block_auth_api.Connection
{
    public interface IUserContractManager
    {
        string AdminAccount();

        Function GetAddUserFunction();


        Function GetUsersFunction();


        Function GetUserCountFunction();


        HexBigInteger GetGasAmount();

        HexBigInteger GetValueAmount();
    }
}