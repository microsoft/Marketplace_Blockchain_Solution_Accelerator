using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Marketplace.BC.Quorum.Service;
using Marketplace.BC.Offchain.Repo.Models;
using Newtonsoft;
using Newtonsoft.Json;

namespace Marketplace.BC.Quorum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartContractController : ControllerBase
    {
        private readonly ISmartContract _SmartContract;
        public SmartContractController(ISmartContract SmartContract)
        {
            _SmartContract = SmartContract;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<HttpResponseMessage> Get(string id)
        {
            return await _SmartContract.CreateResponse(id, "getConsignment");
        }

        // POST api/values
        [HttpPost]
        public async Task<bool> Post(string order, string bindingID)
        {
            var _order = JsonConvert.DeserializeObject<Order>(order);

            //Map Order Object to PlaceOrder smart contract parameters
            PlaceOrder ord = new PlaceOrder
            {
                BindingID = bindingID,
                OrderCode = _order.OrderCode,
                OrderStatus = 0,
                BookingDate = _order.BookingDate.ToString(),
                ServiceType = 0,
                ServiceDate = DateTime.Today.ToShortDateString(),
                PartyID = "PLACE_HOLDER_ID", /* TODO : Ensure mapped to proper value in order */
                Quantity = "PLACE_HOLDER_CONSIGNMENT",
                Price = _order.OrderPrice.ToString(),
                Currency = _order.Currency
            };

            await _SmartContract.CreateResponse(JsonConvert.SerializeObject(ord), "NeoPlaceOrder");
            return true;
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<bool> Put(string value)
        {
            await _SmartContract.CreateResponse(value, "setMilestone");
            return true;
        }
    }
}

