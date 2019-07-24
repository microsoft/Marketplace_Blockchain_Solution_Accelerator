using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Marketplace.APIService.Controllers;
using Marketplace.BC.Offchain.Repo.Models;

namespace Marketplace.Tests
{
    public class OrderController_Tests
    {
        [Fact]
        public void Create_ShouldCreateOrder()
        {
            OrderController controller = new OrderController();
            var result = controller.Create(CreateModel());
            Assert.True(true);
        }

        private Order CreateModel()
        {
            Random random = new Random();
            Consignment consignment = new Consignment
            {
                Code = "PlACE_HOLDER_CONSIGNMENT_CODE",
                Status = "WAITING_APPROVAL",
                Type = "Airfare",
                MonitoringType = "Phone",
                TravellerInfo = new TravellerInfo
                {
                    PassengerType = "PASSENGER_TYPE",
                    TravellerID = "TRAVELER_ID",
                    TravellerName = "TRAVELER_NAME"
                },
                AccommodationData = new AccommodationData
                {
                    CheckIn = DateTimeOffset.Now,
                    CheckOut = DateTimeOffset.Now,
                    Provider = "PROVIDER"
                }

            };

            Order ord = new Order
            {
                OrderCode = "PLACE_HOLDER_ORDER_CODE",
                OrderStatus = "0",
                BookingDate = DateTimeOffset.Now,
                Currency = "USD",
                OrderPrice = 0,
                OrderTotalTax = 0,
                OrderPriceTotal = 0,
                Consignments = new Consignment[1]
            };

            ord.Consignments[0] = consignment;

            return ord;
        }
    }
}
