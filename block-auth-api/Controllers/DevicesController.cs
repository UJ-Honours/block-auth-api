using block_auth_api.Connection;
using block_auth_api.Models;
using block_auth_api.Orchestration.DeviceContract;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Hex.HexTypes;
using RestSharp;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
        public ActionResult GetDevices()
        {
            string sqlIoTDeviceDetails = "SELECT * FROM IoTDevice;";
            var iotDeviceDic = new Dictionary<string, List<Device>>();
            using (var connection = new SqlConnection(_ContractManager.GetConnectionString()))
            {
                var iotDevice = connection.Query<Device>(sqlIoTDeviceDetails).ToList();
                iotDeviceDic.Add("devices", iotDevice);
                return Ok(iotDeviceDic);
            }

        }

        [HttpGet]
        [Route("device/{url}")]
        public ActionResult GetDevice(string url = "http://192.168.8.186:8081") {
            var requestA = new RestRequest()
            {
                Method = Method.GET,
                Resource = "/"
            };
            var client = new RestClient(url);
            var responseA = client.Execute(requestA);
            return Ok(responseA.Content);

        }

        [HttpPost]
        [Route("devices_trigger_event")]
        public ActionResult TriggerEvent()
        {
            var accountAddress = _ContractManager.AdminAccount();
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
        [Route("add_device")]
        public ActionResult AddDevice([FromBody] Device device)
        {
            var accountAddress = _ContractManager.AdminAccount();
            var gas = new HexBigInteger(new BigInteger(400000));
            var value = new HexBigInteger(new BigInteger(0));

            var loginFunction = _ContractManager
                .GetContract()
                .GetFunction("addDevice")
                .SendTransactionAsync(accountAddress, gas, value, device.Name, device.Account);
            loginFunction.Wait();

            string iotDeviceInsert = "INSERT INTO IoTDevice (Name,Account,IP) Values (@Name,@Account,@IP);";

            using (var connection = new SqlConnection(_ContractManager.GetConnectionString()))
            {
                var affectedRows = connection.Execute(iotDeviceInsert, new { device.Name, device.Account,device.Ip });
            }
            return Ok(device);
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
            return Ok(responseB.Data);
        }

        [HttpPost]
        [Route("devices_connect/{url}")]
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
            string url = "http://192.168.8.186:8081";
            var client = new RestClient(url);
            requestC.AddParameter("message", $"{loggedIn.Token}");
            var responseC = client.Execute(requestC);

            return Ok(responseC.Content);
        }

    }
}