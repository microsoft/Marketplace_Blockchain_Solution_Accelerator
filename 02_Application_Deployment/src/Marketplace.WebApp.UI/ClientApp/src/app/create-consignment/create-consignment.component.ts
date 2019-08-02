import { Component, OnInit, ViewChild } from '@angular/core';
import { SelectList } from '../interfaces/selectlist';
import { IOrder } from 'src/api/models/createOrder';
import { IConsignment } from 'src/app/consignment';
import { NgForm } from '@angular/forms';
import { ConsignmentService } from 'src/api/consignment.service';
import { Router } from "@angular/router";

const countries = ['Alabama', 'Alaska', 'American Samoa', 'Arizona', 'Arkansas', 'California', 'Colorado',
  'Connecticut', 'Delaware', 'District Of Columbia', 'Federated States Of Micronesia', 'Florida', 'Georgia',
  'Guam', 'Hawaii', 'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky', 'Louisiana', 'Maine',
  'Marshall Islands', 'Maryland', 'Massachusetts', 'Michigan', 'Minnesota', 'Mississippi', 'Missouri', 'Montana',
  'Nebraska', 'Nevada', 'New Hampshire', 'New Jersey', 'New Mexico', 'New York', 'North Carolina', 'North Dakota',
  'Northern Mariana Islands', 'Ohio', 'Oklahoma', 'Oregon', 'Palau', 'Pennsylvania', 'Puerto Rico', 'Rhode Island',
  'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont', 'Virgin Islands', 'Virginia',
  'Washington', 'West Virginia', 'Wisconsin', 'Wyoming'];

@Component({
  selector: 'app-create-consignment',
  templateUrl: './create-consignment.component.html',
  styleUrls: ['./create-consignment.component.css']
})
export class CreateConsignmentComponent implements OnInit {
  countries: any;
  currentRate: any;
  activityTypes: Array<SelectList>;
  skydivingProviders: Array<SelectList>;
  cardriveProviders: Array<SelectList>;
  transportProviders: Array<SelectList>;
  hotelNames: Array<SelectList>;
  transportActivityTypes: Array<SelectList>;
  minDate: Date;

  createOrderForm: IOrder = {
      bookingDate: new Date(),
      mainSupplier: null,
      hideShowCB: false,
      hideShowAccoFields: false,
      hideShowTransportFields: false,
      activityType: null,
      provider: null,
      activityDate: null,
      hotelName: null,
      checkoutDate: null,
      checkinDate: null,
      arrivalDate: null,
      departureDate: null,
      transportActivityType: null,
      transportProvider: null,
      origin: null,
      destination: null,
      travellerName: null,
      age: null
  } ;

  //Error handling variables
  postError: boolean =false;
  postErrorMessage: string = '';
  noConsignments: boolean = false;

  constructor(private consignmentService: ConsignmentService, private router: Router) { 
  }

  ngOnInit() {

    this.minDate = new Date();

    this.activityTypes = [{
      text: "SkyDiving",
      value: "SkyDiving",
      disabled: false
    },
    {
      text: "CarDrive",
      value: "CarDrive",
      disabled: false
    }]

    this.transportActivityTypes = [{
      text: "Flight",
      value: "Flight",
      disabled: false
    },
    {
      text: "Lounge",
      value: "Lounge",
      disabled: false
    }]
    // },
    // {
    //   text: "Chauffeur",
    //   value: "Chauffeur",
    //   disabled: false
    // }]

    this.transportProviders = [{
    text: "Contoso",
    value: "Contoso",
    disabled: false
  }]

    this.skydivingProviders = [{
    text: "Car_Rentals",
    value: "Car_Rentals",
    disabled: false
    },
    {
      text: "ABC_Hotels",
      value: "ABC_Hotels",
      disabled: false
    }],

    this.cardriveProviders = [
      {
        text: "Car Rentals",
        value: "Car_Rentals",
        disabled: false
      },
      {
        text: "Gen Rentals",
        value: "Gen_Rentals",
        disabled: false
      }
    ],

    this.hotelNames = [{
      text: "BHotel",
      value: "BHotel",
      disabled: false
    },
    {
      text: "AHotel",
      value: "AHotel",
      disabled: false
    }]

    this.countries = countries;
  }

  onHttpError(errorResponse: any) {
    console.log('error: ', errorResponse);
    this.postError = true;
    this.postErrorMessage = errorResponse.error.errorMessage;
  }

  onSubmit(form: NgForm){
    //if neither of one , two or three is set, show an error
    if(!this.createOrderForm.activityType && !this.createOrderForm.hotelName && !this.createOrderForm.transportActivityType){
        this.noConsignments = true;
    }
    else{
        this.postError = false;
        this.noConsignments = false;
        let postData: any;
        postData = {};
        postData.orderCode = Math.random().toString(10).slice(2);
        postData.orderStatus = "ACTIVE"
        postData.bookingDate = new Date().toISOString();
        postData.currency = "USD"
        postData.orderPrice = 100;
        postData.orderTotalTax = 0
        postData.orderPriceTotal = postData.orderPrice + postData.orderTotalTax;
        postData.consignments = [];
        postData.supplier = this.createOrderForm.mainSupplier;
        if(this.createOrderForm.activityType)
        {
          let consignment: any;
          consignment = {};
          consignment.code = "CNS" + Math.random().toString(10).slice(2, 8);
          consignment.status = "READY";
          consignment.type ="ACTIVITY";
          consignment.monitoringType = this.createOrderForm.activityType;
          consignment.activityData = {};
          consignment.activityData.activityOffering = this.createOrderForm.provider + '-' + this.createOrderForm.activityType;
          consignment.activityData.provider = this.createOrderForm.provider;
          consignment.activityData.refNumber = "INV-" + Math.random().toString(10).slice(0, 6);
          //TODO: Need to update this to provider id
          consignment.activityData.bpNumber = 123456;
          consignment.activityData.startDateTime = this.createOrderForm.activityDate.toISOString();
          postData.consignments.push(consignment);
        }
        //if hotel name is set, add those fields.
        if(this.createOrderForm.hotelName)
        {
          let consignment: any;
          consignment = {};
          consignment.code = "ACC-" + Math.random().toString(10).slice(2, 8);
          consignment.status = "READY";
          consignment.type ="ACCOMMODATION";
          consignment.monitoringType = "HOTEL";
          consignment.accommodationData = {};
          consignment.accommodationData.accommodationName = this.createOrderForm.hotelName;
          consignment.accommodationData.provider = this.createOrderForm.provider;
          consignment.accommodationData.ratePlan = "DEFAULT_RATE_PLAN";  
          consignment.accommodationData.checkIn = this.createOrderForm.checkinDate.toISOString();
          consignment.accommodationData.checkOut = this.createOrderForm.checkoutDate.toISOString();
          //TODO: Need to update this to provider id
          consignment.vendorNumber = "1234567"
          postData.consignments.push(consignment);
        }
        //create transport fields
        if(this.createOrderForm.transportActivityType)
        {
          let consignment: any;
          consignment = {};
          consignment.code = "TNS-" + Math.random().toString(10).slice(2, 8);
          consignment.status = "READY";
          consignment.type ="TRANSPORT";         
          consignment.monitoringType = this.createOrderForm.transportActivityType;
          consignment.travellerInfo = {};
          consignment.travellerInfo.travellerId = Math.random().toString(10).slice(2, 8);
          consignment.travellerInfo.travellerName = this.createOrderForm.travellerName;
          consignment.travellerInfo.passengerType = this.createOrderForm.age ? (this.createOrderForm.age > 18 ? 'adult': 'child') : null;
          consignment.travellerInfo.travellerAge = this.createOrderForm.age;
          consignment.itineraryDetails = {};
          consignment.itineraryDetails.provider = this.createOrderForm.transportProvider;
          consignment.itineraryDetails.source = this.createOrderForm.origin;
          consignment.itineraryDetails.departureDateTime = this.createOrderForm.departureDate.toISOString();
          consignment.itineraryDetails.destination = this.createOrderForm.destination;
          consignment.itineraryDetails.arrivalDateTime = this.createOrderForm.arrivalDate.toISOString();
          postData.consignments.push(consignment);
        }


        //do the necessary mapping, call the create service
        this.consignmentService.postOrder(postData).subscribe(
          result => {
            this.router.navigate(['/consignment/' + postData.consignments[0].code], { queryParams: { isAdmin : false }} )
          },
          error  => this.onHttpError(error)
        );
    }
  }
}
