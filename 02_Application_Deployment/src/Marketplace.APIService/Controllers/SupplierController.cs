namespace Marketplace.APIService.Controllers
{
    using Marketplace.APIService.Models;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Swagger;
    using System;
    using System.Collections.Generic;
    using Marketplace.BC.Offchain.Repo.Models;
    using Newtonsoft.Json;

    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class SupplierController : Controller
    {
        private IBusinessLogicLayer _businessLogicLayer;
        private IServiceAgent _service;
        private ITransactionIndexerServiceAgent _txAgent;

        public SupplierController(IServiceAgent serviceAgent, ITransactionIndexerServiceAgent txAgent, IBusinessLogicLayer businessLayer)
        {
            _businessLogicLayer = businessLayer;
            _service = serviceAgent;
            _txAgent = txAgent;
        }

        [HttpGet]
        public IActionResult GetConsignmentForLoggedInSupplier(string supplierId)
        {
            var transactions = _txAgent.GetTransactionBySupplierId(supplierId);

            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return Ok(transactions); 
        }

        [HttpPut]
        public IActionResult SupplierApproval(string supplierId, string orderID, string status, string consignmentCode)
        {
            var result = _businessLogicLayer.UpdateSupplierStatus(orderID, consignmentCode, supplierId, status);
            SetMilestone milestone = new SetMilestone
            {
                ID = orderID,
                Value = 2
            };
            _service.SmartContract3Async(JsonConvert.SerializeObject(milestone));
            return Ok(true);
        }
    }
}
