using Marketplace.BC.Offchain.Repo.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Marketplace.APIService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private IServiceAgent _service;


        //private ServiceAgent serviceAgent;
        private ITransactionIndexerServiceAgent _txAgent;

        public OrderController(IServiceAgent serviceAgent, ITransactionIndexerServiceAgent txAgent)
        {
            _service = serviceAgent;
            _txAgent = txAgent;
        }

        /// <summary>
        /// Pay & place order
        /// </summary>

        /// <returns>Contract ID</returns>
        [HttpPost]
        public IActionResult Create([FromBody] Order order)
        {
            if (order == null)
                return BadRequest();

            var newBindingID = Guid.NewGuid();

            var temp = JsonConvert.SerializeObject(order);
            _service.SmartContract2Async(temp, newBindingID.ToString());

            var _simulatedDeployConfirmation = prepareDeployObject(newBindingID.ToString(), order);
            _txAgent.LogTransaction(_simulatedDeployConfirmation);

            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            return CreatedAtRoute("GetOrder", new { id = newBindingID }, order);
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult GetByContractId(Guid id)
        {
            var transactions = _txAgent.GetTransaction(id.ToString());
            return Ok(transactions);
        }

        [HttpGet("Vendors/All")]
        public IActionResult GetAllVendors()
        {
            var transactions = _txAgent.GetVendors();
            return Ok(transactions);
        }

        [HttpGet("OrderCode/{id}")]
        public IActionResult GetByOrderCode(string id)
        {
            var transactions = _txAgent.GetTransactionByOrderCode(id);
            return Ok(transactions);
        }

        [HttpGet("Consignment/{id}")]
        public IActionResult GetByConsignmentCode(string id)
        {
            var transactions = _txAgent.GetTransactionByConsignmentCode(id);
            return Ok(transactions);
        }


        private static ContractTransaction prepareDeployObject(string bindingID, Order orderObject)
        {
            var accomodation = orderObject.Consignments[0].AccommodationData;
            var activity = orderObject.Consignments[0].ActivityData;


            SampleDTO _dto = new SampleDTO()
            {
                ContractID = bindingID,
                TransactionID = bindingID,
                TransactedTime = DateTime.Now,
                Description = "Transacted",
                AdminDetail = new AdminDetails()
                {
                    OrderCode = orderObject.OrderCode,
                    ConsignmentCode = orderObject.Consignments[0].Code,
                    ConsignmentStatus = orderObject.Consignments[0].Status,
                    BookingDate = orderObject.BookingDate.ToString(),
                    CheckinDate = accomodation == null ? null : accomodation.CheckIn.ToString(),
                    CheckoutDate = accomodation == null ? null : accomodation.CheckOut.ToString(),
                    Currency = orderObject.Currency,
                    Quantity = "1",
                    Price = orderObject.OrderPrice.ToString(),
                    ServiceDate = DateTime.Now.ToString(),
                    ServiceType = orderObject.Consignments[0].Type,
                    Supplier = orderObject.Supplier
                }
            };
            ConstructorConfirmation _deployConfirmation = new ConstructorConfirmation()
            {
                BindingId = bindingID,
                Name = "New Enterprise Smart Contract instance",
                TransactionConfirmation = new TransactionConfirmation()
                {
                    BlockHash = "0xfa4e2a31506c1f930efc7701ff6ddc1451d08a38a7a9267fe263766b4c7ea2d0",
                    BlockNumber = "1",
                    ContractAddress = "0xed9d02e382b34818e88b88a309c7fe71e65f419d",
                    TransactionHash = "",
                    ContractName = "",
                    ProxyId = "",
                    TransactionIndex = "1"
                }
            };
            ContractTransaction _txInformation = new ContractTransaction(_dto, _deployConfirmation);

            _txInformation.OrderDetails = orderObject;

            return _txInformation;
        }
    }
}