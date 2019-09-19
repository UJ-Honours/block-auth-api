using block_auth_api.Connection;
using block_auth_api.Models;

namespace block_auth_api.Orchestration
{
    public class RoleOrchestration : IRoleOrchestration
    {
        private readonly IRoleContractManager _ContractManager;

        public RoleOrchestration(IRoleContractManager contractManager)
        {
            _ContractManager = contractManager;
        }

        public Role GetGuestRole()
        {
            var grFunction = _ContractManager
                .GetGuestRoleFunction()
                .CallAsync<Role>();

            grFunction.Wait();

            var guestRole = grFunction.Result;

            return guestRole;
        }

        public Role GetOwnerRole()
        {
            var orFunction = _ContractManager
                .GetOwnerRoleFunction()
                .CallAsync<Role>();

            orFunction.Wait();

            var ownerRole = orFunction.Result;

            return ownerRole;
        }

        public bool UpdateGuestRole(Role role)
        {
            var accountAddress = _ContractManager.AdminAccount();
            var gas = _ContractManager.GetGasAmount();
            var value = _ContractManager.GetValueAmount();

            var loginFunction = _ContractManager
                .GetUpdateGuestRoleFunction()
                .SendTransactionAsync(accountAddress, gas, value, role.On, role.Off);

            loginFunction.Wait();

            return true;
        }

        public bool UpdateOwnerRole(Role role)
        {
            var accountAddress = _ContractManager.AdminAccount();
            var gas = _ContractManager.GetGasAmount();
            var value = _ContractManager.GetValueAmount();

            var loginFunction = _ContractManager
                .GetUpdateOwnerRoleFunction()
                .SendTransactionAsync(accountAddress, gas, value, role.On, role.Off);

            loginFunction.Wait();

            return true;
        }
    }
}
