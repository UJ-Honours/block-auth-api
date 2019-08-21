using block_auth_api.Models;
using block_auth_api.Orchestration.DeviceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace block_auth_api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]"), Authorize]
    public class DevicesController : Controller
    {
        private readonly IDeviceContractOrchestration _DCO;

        public DevicesController(IDeviceContractOrchestration dco)
        {
            _DCO = dco;
        }

        [HttpGet]
        [Route("devices")]
        public ActionResult GetDevices()
        {
            try
            {
                var deviceDictionary = _DCO.GetDevices();
                return Ok(deviceDictionary);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("device/{url}")]
        public ActionResult GetDevice(string url = "http://192.168.8.186:8081")
        {
            try
            {
                var result = _DCO.GetDevice(url);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("devices_trigger_event")]
        public ActionResult TriggerEvent()
        {
            try
            {
                _DCO.TriggerEvent();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add_device")]
        public ActionResult AddDevice([FromBody] Device device)
        {
            try
            {
                _DCO.AddDevice(device);

                return Ok(device);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("devices_auth/{url}")]
        public ActionResult DeviceAuth(string url = "http://192.168.8.186:8081")
        {
            try
            {
                var result = _DCO.DeviceAuth(url);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("devices_connect/{url}")]
        public ActionResult AccessDevice([FromBody] LoggedIn loggedIn, string url = "http://192.168.8.186:8081")
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var result = _DCO.AccessDevice(loggedIn, url);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}