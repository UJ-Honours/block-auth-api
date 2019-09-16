using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;

namespace block_auth_api.Connection
{
    public interface ITaskContractManager
    {
        Function GetTasksFunction();

        Function GetTaskCountFunction();

        Function GetCreateTaskFunction();

        Function GetToggleTaskFunction();

        HexBigInteger GetGasAmount();

        HexBigInteger GetValueAmount();

        string AdminAccount();
    }
}
