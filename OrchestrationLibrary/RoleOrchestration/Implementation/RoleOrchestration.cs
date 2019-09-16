using block_auth_api.Connection;
using block_auth_api.Models;
using System.Collections.Generic;
using System.Numerics;

namespace block_auth_api.Orchestration
{
    public class RoleOrchestration : IRoleOrchestration
    {
        private readonly IRoleContractManager _ContractManager;

        public RoleOrchestration(IRoleContractManager contractManager)
        {
            _ContractManager = contractManager;
        }

        public Role GetRole(int index)
        {
            var result = _ContractManager
                    .GetRolesFunction()
                    .CallDeserializingToObjectAsync<Role>(index);
            result.Wait();
            return result.Result;
        }

        public int GetRoleCount()
        {
            var roleCountFunction = _ContractManager
                .GetRoleCountFunction()
                .CallAsync<BigInteger>();
            roleCountFunction.Wait();
            var roleCount = (int)roleCountFunction.Result;
            return roleCount;
        }

        public void CreateRole(string name)
        {
            var accountAddress = _ContractManager.AdminAccount();
            var gas = _ContractManager.GetGasAmount();
            var value = _ContractManager.GetValueAmount();

            var loginFunction = _ContractManager
                .GetCreateRoleFunction()
                .SendTransactionAsync(accountAddress, gas, value, name);
            loginFunction.Wait();
        }

        public Dictionary<string, List<Role>> GetRoles()
        {
            var roleDictionary = new Dictionary<string, List<Role>>();
            var roleList = new List<Role>();

            var roleCount = GetRoleCount();

            for (int i = 0; i <= roleCount; i++)
            {
                var role = GetRole(i);
                roleList.Add(role);
            }

            roleDictionary.Add("roles", roleList);
            return roleDictionary;
        }
    }
}
