using block_auth_api.Models;
using block_auth_api.Orchestration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace block_auth_api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly IOwnerRoleOrchestration _ORO;
        private readonly IGuestRoleOrchestration _GRO;

        public RolesController(IOwnerRoleOrchestration oro, IGuestRoleOrchestration gro)
        {
            _ORO = oro;
            _GRO = gro;
        }

        [HttpGet]
        [Route("get_owner_roles")]
        public ActionResult GetOwnerRoles()
        {
            var ownerRole = new OwnerRole() { 
                Off= _ORO.GetOwnerRoleOff(),
                On = _ORO.GetOwnerRoleOn()
            };
            return Ok(ownerRole);
        }

        [HttpGet]
        [Route("get_guest_roles")]
        public ActionResult GetGuestRoles()
        {
            var guestRole = new GuestRole()
            {
                Off = _GRO.GetGuestRoleOff(),
                On = _GRO.GetGuestRoleOn()
            };
            return Ok(guestRole);
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

                var success = _ORO.UpdateOwnerRole(role);

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

                var success = _GRO.UpdateGuestRole(role);

                if (success)
                {
                    return Ok("Guest Permissions Changed Successfully");
                }
                else
                {
                    return BadRequest("Something wrong happened");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
