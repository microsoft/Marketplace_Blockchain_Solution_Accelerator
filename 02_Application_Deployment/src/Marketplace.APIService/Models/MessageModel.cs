using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.APIService.Models
{
    public class MessageModel
    {
        public string OrderCode { get; set; }

        public DateTime BookingDate { get; set; }

        public string Currency { get; set; }

        public string ServiceType { get; set; }

        public string MonitoringType { get; set; }

        public string Provider { get; set; }

        public string VendorName { get; set; }
    }
}
