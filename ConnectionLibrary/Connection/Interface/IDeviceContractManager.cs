using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;

namespace block_auth_api.Connection
{
    public interface IDeviceContractManager
    {
        string AdminAccount();

        Function GetLoginAdminFunction();


        Function GetAddDeviceFunction();


        Function GetDevicesFunction();


        Function GetDeviceCountFunction();

        HexBigInteger GetGasAmount();

        HexBigInteger GetValueAmount();
    }
}