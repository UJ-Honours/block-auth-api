using block_auth_api.Orchestration.DeviceContract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace block_auth_api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DeviceController : Controller
    {
        private readonly IDeviceContractOrchestration _DCO;

        public DeviceController(IDeviceContractOrchestration dco)
        {
            _DCO = dco;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "device1", "device2" };
        //}

        //[HttpGet]
        //[Route("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return $"device{id}";
        //}

        [HttpGet]
        public ActionResult<int> GetNumDevices()
        {
            return _DCO.GetNumDevices();
        }
    }
}