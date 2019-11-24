using block_auth_api.Connection;
using block_auth_api.Models;
using System;

namespace block_auth_api.Orchestration
{
    public class GuestRoleOrchestration : IGuestRoleOrchestration
    {
        private readonly IGuestRoleContractManager _ContractManager;

        public GuestRoleOrchestration(IGuestRoleContractManager contractManager)
        {
            _ContractManager = contractManager;
        }

        public bool GetGuestRoleOff()
        {
            var grFunction = _ContractManager
                .GetOffFunction()
                .CallAsync<bool>();

            grFunction.Wait();

            var off = grFunction.Result;

            return off;
        }

        public bool GetGuestRoleOn()
        {
            var grFunction = _ContractManager
                .GetOnFunction ()
                .CallAsync<bool>();

            grFunction.Wait();

            var on = grFunction.Result;

            return on;
        }

        public bool UpdateGuestRole(RolePermission role)
        {
            try
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
            catch (Exception)
            {
                return false;
            }

        }
    }
}
