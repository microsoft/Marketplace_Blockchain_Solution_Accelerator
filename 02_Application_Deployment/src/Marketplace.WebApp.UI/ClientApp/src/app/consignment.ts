export interface IConsignment {
            
            OrderCode       : string,
            ConsignmentCode : string,  
            OrderStatus : string,
            ConsignmentStatus : string,
            ActivityName: String,
            BookingDate : Date, 
            ServiceType : string,
            ServiceDate : Date,
            Supplier    : string,
            CheckinDate : Date,
            CheckoutDate : Date,
            Quantity     : number,
            Price          : number,
            Currency     : string,
            HotelName: string,
            ArrivalDate: Date,
            DepartureDate: Date,
            TransportActivityType: string,
            TransportProvider: string,
            Origin: string,
            Destination: string,
            TravellerName: string,
            Age: number,
            PassengerType: string

}