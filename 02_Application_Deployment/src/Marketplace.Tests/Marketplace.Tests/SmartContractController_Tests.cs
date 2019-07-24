using System;
using Xunit;
using Marketplace.BC.Quorum.API.Controllers;
using Marketplace.BC.Offchain.Repo.Models;
using Marketplace.BC.Quorum.Service;
using Newtonsoft.Json;

namespace Marketplace.Tests
{
    public class SmartContractController_Tests
    {

        [Fact]
        public async void Post_ShouldCreateOrder()
        {
            SmartContract smartContract = new SmartContract();
         
            var controller = new SmartContractController(smartContract);
            string order = CreateModel();
            var result = await controller.Post(order, "fd0a587a-3ba0-433e-b42e-de9a5d93f65e");

            Assert.True(result);
        }

        [Fact]
        public async void Get_ShouldGetOrder()
        {
            SmartContract smartContract = new SmartContract();

            var controller = new SmartContractController(smartContract);
            string order = CreateModel();
            var argument = "{\"bindingID\":\"PLACE_HOLDER_ORDER_CODE\"}";
            var result = await controller.Get(argument);
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async void Put_ShouldUpdateOrder()
        {
            SmartContract smartContract = new SmartContract();

            var controller = new SmartContractController(smartContract);
            string milestone = CreateSetMilestone();
            var result = await controller.Put(milestone);
            Assert.True(result);
        }

        private string CreateModel()
        {
            Random random = new Random();

            Order ord = new Order
            {
                OrderCode = "PLACE_HOLDER_ORDER_CODE",
                OrderStatus = "0",
                BookingDate = DateTimeOffset.Now,
                Currency = "USD",
                OrderPrice = 0, 
                OrderTotalTax= 0,
                OrderPriceTotal= 0
            };
            return JsonConvert.SerializeObject(ord);
        }

        private string CreateSetMilestone()
        {
            SetMilestone milestone = new SetMilestone
            {
                Value = 1,
                ID = "PLACE_HOLDER_ORDER_CODE"
            };

            return JsonConvert.SerializeObject(milestone);
        }
    }


}
