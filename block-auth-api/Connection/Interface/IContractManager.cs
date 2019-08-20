using Nethereum.Contracts;

namespace block_auth_api.Connection
{
    public interface IContractManager
    {
        string AdminAccount();

        Function GetLoginAdminFunction();

        Function GetAddUserFunction();

        Function GetAddDeviceFunction();

        Function GetUsersFunction();

        Function GetDevicesFunction();

        Function GetDocumentsFunction();

        Function GetStoreDocumentFunction();

        Function GetUserCountFunction();

        Function GetDeviceCountFunction();
    }
}