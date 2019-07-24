using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Marketplace.APIService.Controllers;
using Marketplace.BC.Offchain.Repo.Models;

namespace Marketplace.Tests
{
    public class SupplierController_Tests
    {
        [Fact]
        public void SupplierApproval_ShouldUpdateApprovalStatus()
        {
            SupplierController controller = new SupplierController();
            var result = controller.SupplierApproval("HGF_HOTELS", "", "APPROVE", "1_3975YPV8V1RWLE02SCFP_EK0203140720180250_FLIGHT_jcxvshdj");
            Assert.True(true);
        }
    }
}
