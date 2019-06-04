using block_auth_api.Orchestration.DeviceContract;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<int> GetNumDevices()
        {
            return _DCO.GetNumDevices();
        }
    }
}