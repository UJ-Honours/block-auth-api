using block_auth_api.Connection;
using block_auth_api.Models;
using block_auth_api.Orchestration.DeviceContract;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Hex.HexTypes;
using RestSharp;
using System.Numerics;

namespace block_auth_api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DevicesController : Controller
    {
        private readonly IDeviceContractOrchestration _DCO;
        private readonly IContractManager _ContractManager;
        
        public DevicesController(IDeviceContractOrchestration dco, IContractManager contractManager)
        {
            _DCO = dco;
            _ContractManager = contractManager;
        }

        [HttpGet]
        [Route("devices")]
        public ActionResult GetDevice() {
            var requestA = new RestRequest()
            {
                Method = Method.GET,
                Resource = "/"
            };
            var client = new RestClient("http://192.168.8.186:8081");
            var responseA = client.Execute(requestA);
            return Ok(responseA.Content);
        }

        [HttpPost]
        [Route("devices_trigger_event")]
        public ActionResult TriggerEvent()
        {
            var accountAddress = "0x9ada8c4979caad44fe7a2b6fb6a45bcd67b8657e";
            var gas = new HexBigInteger(new BigInteger(400000));
            var value = new HexBigInteger(new BigInteger(0));

            var loginFunction = _ContractManager
                .GetContract()
                .GetFunction("login_admin")
                .SendTransactionAsync(accountAddress, gas, value);
            loginFunction.Wait();

            return Ok();
        }

        [HttpPost]
        [Route("devices_auth")]
        public ActionResult DeviceAuth() {
            var requestB = new RestRequest()
            {
                Method = Method.POST,
                Resource = "/auth_data"
            };
            var client = new RestClient("http://192.168.8.186:8081");
            var responseB = client.Post<LoggedIn>(requestB);
            return Ok(responseB.Content);
        }

        [HttpPost]
        [Route("devices_connect")]
        public ActionResult AccessDevice([FromBody] LoggedIn loggedIn)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            var requestC = new RestRequest()
            {
                Method = Method.POST,
                Resource = "/connect"
            };

            var client = new RestClient("http://192.168.8.186:8081");
            requestC.AddParameter("message", $"{loggedIn.Token}");
            var responseC = client.Execute(requestC);

            return Ok(responseC.Content);
        }

    }
}