using block_auth_api.Models;
using block_auth_api.Orchestration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace block_auth_api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]"), Authorize]
    public class RolesController : Controller
    {
        private readonly IRoleOrchestration _RO;

        public RolesController(IRoleOrchestration ro)
        {
            _RO = ro;
        }

        [HttpPost]
        [Route("update_owner_role_permissions")]
        public ActionResult UpdateOwnerRolePermissions([FromBody] RolePermission role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Passed");
                }

                var success = _RO.UpdateOwnerRole(role);

                if (success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Something went wrong");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("update_guest_role_permissions")]
        public ActionResult UpdateGuestRolePermissions([FromBody] RolePermission role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Passed");
                }

                var success = _RO.UpdateGuestRole(role);

                if (success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Something went wrong");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
