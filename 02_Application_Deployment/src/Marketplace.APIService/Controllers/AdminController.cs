namespace Marketplace.APIService.Controllers
{
    using Marketplace.APIService;
    using Marketplace.APIService.Models;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Security.Authentication;

    [Produces("application/json")]
    [Route("api/[Controller]")]

    public class AdminController : Controller
    {
        private IBusinessLogicLayer _businessLogicLayer;

        public AdminController(IBusinessLogicLayer businessLogicLayer)
        {
            _businessLogicLayer = businessLogicLayer;
           
        }

        [HttpGet]
        public IActionResult GetAllOrders(int numberOfConsignmentToShow = 10)
        {
            var dateAndTime = DateTime.Now;
            var result = this._businessLogicLayer.GetAllAdminDetails(0);
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return Ok(result);
        }
    }
}
