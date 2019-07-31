using block_auth_api.Connection;
using block_auth_api.Models;
using block_auth_api.Orchestration.DeviceContract;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Hex.HexTypes;
using Nethereum.Signer;
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

        [HttpPost]
        [Route("devices")]
        public ActionResult AccessDevice()
        {
            var requestA = new RestRequest()
            {
                Method = Method.GET,
                Resource = "/"
            };
            var requestB = new RestRequest()
            {
                Method = Method.POST,
                Resource = "/auth_data"
            };
            var requestC = new RestRequest()
            {
                Method = Method.POST,
                Resource = "/connect"
            };

            var client = new RestClient("http://192.168.8.106:8081");

            var responseA = client.Execute(requestA);
            
            if (responseA.IsSuccessful) {
                var accountAddress = "0x579d5f81373fc3ee5debd5957e82699587735be9";
                var gas = new HexBigInteger(new BigInteger(400000));
                var value = new HexBigInteger(new BigInteger(0));

                var loginFunction = _ContractManager
                    .GetContract()
                    .GetFunction("login_admin")
                    .SendTransactionAsync(accountAddress, gas, value);
                loginFunction.Wait();

                var responseB = client.Post<LoggedIn>(requestB);

                if (responseB.IsSuccessful)
                {
                    var token = responseB.Data.Token;

                    requestC.AddParameter("message", $"{token}");

                    var responseC = client.Execute(requestC);

                    if (responseC.IsSuccessful)
                    {
                        return Ok("All Good");
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
                
            }
            else
            {
                return BadRequest();
            }

        }

    }
}