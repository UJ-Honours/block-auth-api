using block_auth_api.Models;
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

        [HttpGet]
        [Route("Devices")]
        public ActionResult<int> GetNumDevices()
        {
            return _DCO.GetNumDevices();
        }

        
        [HttpGet]
        public ActionResult<List<Device>> Get()
        {
            return new List<Device>();
        }

        [HttpPost]
        public ActionResult<Device> AddDevice()
        {
            return new Device { };
        }

        [HttpDelete]
        public ActionResult<Device> DeleteDevice()
        {
            return new Device { };
        }

        [HttpPut]
        public ActionResult<Device> UpdateDevice()
        {
            return new Device { };
        }
    }
}