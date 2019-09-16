using block_auth_api.Models;
using block_auth_api.Orchestration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace block_auth_api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly ITokenOrchestration _TokenOrchestration;

        public TokenController(ITokenOrchestration tokenOrchestration)
        {
            _TokenOrchestration = tokenOrchestration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("create_token")]
        public ActionResult CreateToken([FromBody]User login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Passed");
                }
                var user = _TokenOrchestration.Authenticate(login);
                if (user == null)
                {
                    return Unauthorized();
                }
                var tokenString = _TokenOrchestration.BuildToken(login);
                user.Token = tokenString;
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}