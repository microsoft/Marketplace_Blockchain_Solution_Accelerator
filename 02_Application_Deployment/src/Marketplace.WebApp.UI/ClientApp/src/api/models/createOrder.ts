export interface IOrder{
    mainSupplier: string;
    checkoutDate: Date;
    checkinDate: Date;
    bookingDate: Date,
    hideShowCB: boolean,
    hideShowAccoFields: boolean,
    hideShowTransportFields: boolean,
    activityType: string,
    provider: string,
    activityDate: Date,
    hotelName: string,
    arrivalDate: Date,
    departureDate: Date,
    transportActivityType: string,
    transportProvider: string,
    origin: string,
    destination: string,
    travellerName: string,
    age: number
}