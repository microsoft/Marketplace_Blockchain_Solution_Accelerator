using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Marketplace.BC.Offchain.Repo.Models
{

    public class Order
    {
        /// <summary>
        /// Order code
        /// </summary>
        [JsonProperty(PropertyName = "orderCode")]
        [Required]
        [StringLength(6)]
        public string OrderCode { get; set; }

        [JsonProperty(PropertyName = "orderStatus")]
        public string OrderStatus { get; set; }

        [JsonProperty(PropertyName = "bookingDate")]
        public DateTimeOffset? BookingDate { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "orderPrice")]
        public uint OrderPrice { get; set; }

        [JsonProperty(PropertyName = "orderTotalTax")]
        public uint OrderTotalTax { get; set; }

        [JsonProperty(PropertyName = "orderPriceTotal")]
        public uint OrderPriceTotal { get; set; }

        [JsonProperty(PropertyName = "supplier")]
        public string Supplier { get; set; }

        [JsonProperty(PropertyName = "consignments")]
        public Consignment[] Consignments { get; set; }
    }

    public class Consignment
    {

        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "monitoringType")]
        public string MonitoringType { get; set; }

        [JsonProperty(PropertyName = "travellerInfo")]
        public TravellerInfo TravellerInfo { get; set; }

        [JsonProperty(PropertyName = "itineraryDetails")]
        public ItineraryDetails ItineraryDetails { get; set; }

        [JsonProperty(PropertyName = "accommodationData")]
        public AccommodationData AccommodationData { get; set; }

        [JsonProperty(PropertyName = "activityData")]
        public ActivityData ActivityData { get; set; }


        [JsonProperty(PropertyName = "entries")]
        public Entrie[] Entries { get; set; }


    }

    public class Entrie
    {
        
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
        
        
        [JsonProperty(PropertyName = "orderEntry")]
        public OrderEntrie OrderEntrie { get; set; }
    }  
    
    public class OrderEntrie
    {
        
        [JsonProperty(PropertyName = "orderEntryNumber")]
        public uint OrderEntryNumber { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public uint Quantity { get; set; }

        [JsonProperty(PropertyName = "unitPrice")]
        public uint UnitPrice { get; set; }

        [JsonProperty(PropertyName = "totalPrice")]
        public uint TotalPrice { get; set; }

        [JsonProperty(PropertyName = "basePrice")]
        public uint BasePrice { get; set; }

        [JsonProperty(PropertyName = "bundleNo")]
        public uint BundleNo { get; set; }

        [JsonProperty(PropertyName = "principalVsAgent")]
        public string PrincipalVsAgent { get; set; }

        [JsonProperty(PropertyName = "revenueStatus")]
        public string RevenueStatus { get; set; }

        [JsonProperty(PropertyName = "taxes")]
        public Tax[] Taxes { get; set; }

        [JsonProperty(PropertyName = "adjustments")]
        public Adjustment[] Adjustment { get; set; }
    }

    public class Tax
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "value")]
        public uint Value { get; set; }

        [JsonProperty(PropertyName = "appliedValue")]
        public uint AppliedValue { get; set; }
    }

    public class Adjustment
    {
        [JsonProperty(PropertyName = "value")]
        public uint Value { get; set; }

        [JsonProperty(PropertyName = "adjustmentType")]
        public string AdjustmentType { get; set; }
    }


    public class TravellerInfo
    {
        [JsonProperty(PropertyName = "travellerId")]
        public string TravellerID { get; set; }

        [JsonProperty(PropertyName = "travellerName")]
        public string TravellerName { get; set; }

        [JsonProperty(PropertyName = "travellerAge")]
        public int TravellerAge { get; set; }

        [JsonProperty(PropertyName = "passengerType")]
        public string PassengerType { get; set; }
    }

    public class ItineraryDetails
    {
        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }

        [JsonProperty(PropertyName = "destination")]
        public string Destination { get; set; }

        [JsonProperty(PropertyName = "departureDateTime")]
        public DateTimeOffset? DepartureDateTime { get; set; }

        [JsonProperty(PropertyName = "arrivalDateTime")]
        public DateTimeOffset? ArrivalDateTime { get; set; }

        [JsonProperty(PropertyName = "provider")]
        public string provider { get; set; }
    }

    public class ActivityData
    {
        [JsonProperty(PropertyName = "activityOffering")]
        public string ActivityOffering { get; set; }

        [JsonProperty(PropertyName = "provider")]
        public string Provider { get; set; }

        [JsonProperty(PropertyName = "bpNumber")]
        public string BpNumber { get; set; }

        [JsonProperty(PropertyName = "refNumber")]
        public string RefNumber { get; set; }

        [JsonProperty(PropertyName = "startDateTime")]
        public string StartDateTime { get; set; }
    }

    public class AccommodationData
    {
        [JsonProperty(PropertyName = "accommodationName")]
        public string AccommodationName { get; set; }

        [JsonProperty(PropertyName = "provider")]
        public string Provider { get; set; }

        [JsonProperty(PropertyName = "ratePlan")]
        public string RatePlan { get; set; }

        [JsonProperty(PropertyName = "checkIn")]
        public DateTimeOffset? CheckIn { get; set; }

        [JsonProperty(PropertyName = "checkOut")]
        public DateTimeOffset? CheckOut { get; set; }

        [JsonProperty(PropertyName = "vendorNumber")]
        public uint VendorNumber { get; set; }
    }

    public class PlaceOrder
    {
        [JsonProperty(PropertyName = "bindingID")]
        public string BindingID { get; set; }

        [JsonProperty(PropertyName = "orderCode")]
        public string OrderCode { get; set; }

        [JsonProperty(PropertyName = "orderStatus")]
        public int OrderStatus { get; set; }

        [JsonProperty(PropertyName = "bookingDate")]
        public string BookingDate { get; set; }

        [JsonProperty(PropertyName = "serviceType")]
        public int ServiceType { get; set; }

        [JsonProperty(PropertyName = "serviceDate")]
        public string ServiceDate { get; set; }

        [JsonProperty(PropertyName = "partyID")]
        public string PartyID { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public string Quantity { get; set; }

        [JsonProperty(PropertyName = "price")]
        public string Price { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }
    }

    public class SetMilestone
    {
        [JsonProperty(PropertyName = "ID")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "Value")]
        public int Value { get; set; }
    }

}
