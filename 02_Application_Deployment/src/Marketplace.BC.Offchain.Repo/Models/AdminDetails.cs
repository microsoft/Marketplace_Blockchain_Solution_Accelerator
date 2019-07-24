using System;

namespace Marketplace.BC.Offchain.Repo.Models
{
    public class AdminDetails
    {
        //Key for Mongo......
        public Guid Id { get; set; }

        public string OrderCode { get; set; }

        public string ConsignmentStatus { get; set; }

        public string ConsignmentCode { get; set; }

        public string BookingDate { get; set; }

        public string ServiceType { get; set; }

        public string ServiceDate { get; set; }

        public string CheckinDate { get; set; }

        public string CheckoutDate { get; set; }

        public string Supplier { get; set; }

        public string Quantity { get; set; }

        public string Price { get; set; }

        public string Currency { get; set; }
    }
}
