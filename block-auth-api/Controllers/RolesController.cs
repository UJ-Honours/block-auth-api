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
        private readonly IRoleOrchestration _RO;

        public RolesController(IRoleOrchestration ro)
        {
            _RO = ro;
        }

        [HttpGet]
        [Route("get_roles")]
        public ActionResult GetDevices()
        {
            try
            {
                var roleDictionary = _RO.GetRoles();
                return Ok(roleDictionary);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("update_role_permissions")]
        public ActionResult UpdateRolePermissions()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Passed");
                }

                _RO.CreateRole("owner");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
