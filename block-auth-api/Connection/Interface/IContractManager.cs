using Nethereum.Contracts;

namespace block_auth_api.Connection
{
    public interface IContractManager
    {
        string AdminAccount();

        string GetConnectionString();

        Function GetLoginAdminFunction();

        Function GetAddUserFunction();

        Function GetAddDeviceFunction();

        Function GetUsersFunction();

        Function GetDevicesFunction();
    }
}