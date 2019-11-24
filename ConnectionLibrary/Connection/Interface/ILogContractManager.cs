using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;

namespace block_auth_api.Connection
{
    public interface ILogContractManager
    {
        string AdminAccount();

        Function GetAddLogFunction();

        Function GetLogsFunction();

        Function GetLogsCountFunction();

        HexBigInteger GetGasAmount();

        HexBigInteger GetValueAmount();
    }
}