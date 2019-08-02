import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IConsignment } from '../consignment';
import { ConsignmentService } from 'src/api/consignment.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-view-consignment',
  templateUrl: './view-consignment.component.html',
  styleUrls: ['./view-consignment.component.css']
})
export class ViewConsignmentComponent implements OnInit {
  pageTitle: string = '';
  consignment: IConsignment;
  consignmentStates: Map<string, object>;
  isAdmin: boolean = false;
  showStatus: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private consignmentService: ConsignmentService,
    private location: Location
  ) {}

  ngOnInit() {
    let consignmentCode = this.route.snapshot.paramMap.get('id');
    let isAdmin = this.route.snapshot.queryParamMap.get('isAdmin');
    if (isAdmin == 'false') {
      this.isAdmin = false;
    } else {
      this.isAdmin = true;
    }
    //call service to get consignment details, get from parent compo?
    this.pageTitle += `Consignment: ${consignmentCode} `;

    //Fallback dummy data
    this.consignment = {
      OrderCode: 'EGSF7',
      ConsignmentCode: 'ABC102',
      OrderStatus: 'ACTIVE',
      ConsignmentStatus: 'ACTIVE',
      ActivityName: 'SkyDiving',
      BookingDate: new Date('2018-10-25'),
      ServiceType: 'ACTIVITY',
      ServiceDate: new Date('2018-11-30'),
      Supplier: 'Contoso',
      CheckinDate: new Date('2018-11-30'),
      CheckoutDate: new Date('2018-11-30'),
      Quantity: 1,
      Price: 1200,
      Currency: 'USD',
      HotelName: 'BHotel',
      ArrivalDate: new Date('2019-10-16'),
      DepartureDate: new Date('2019-10-15'),
      TransportActivityType: 'Flight',
      TransportProvider: 'Contoso',
      Origin: 'Paris',
      Destination: 'London',
      TravellerName: 'T.J.James',
      Age: 20,
      PassengerType: 'adult'
    };

    this.getConsignment(consignmentCode);
  }

  getConsignment(consignmentCode) {
    this.consignmentService
      .getConsignmentByID(consignmentCode)
      .subscribe((res: any) => {
        if (res && res[0]) {
          let length = res.length;
          let con = res[length - 1];
          //last data with that id is
          this.consignment.OrderCode =
            con.businessContractDTO.adminDetail.orderCode;
          this.consignment.ConsignmentCode =
            con.businessContractDTO.adminDetail.consignmentCode;
          this.consignment.ConsignmentStatus =
            con.businessContractDTO.adminDetail.consignmentStatus;
          this.consignment.BookingDate =
            con.businessContractDTO.adminDetail.bookingDate;
          this.consignment.ServiceDate =
            con.businessContractDTO.adminDetail.serviceDate;
          this.consignment.ServiceType =
            con.businessContractDTO.adminDetail.serviceType;
          this.consignment.Price = con.businessContractDTO.adminDetail.price;
          this.consignment.Supplier =
            con.businessContractDTO.adminDetail.supplier;
          this.consignment.Quantity =
            con.businessContractDTO.adminDetail.quantity;
          if (this.consignment.ServiceType == 'ACTIVITY') {
            this.consignment.ActivityName =
              con.orderDetails.consignments[0].monitoringType;
          }

          //Fields for ACC Type
          if (this.consignment.ServiceType == 'ACCOMMODATION') {
            this.consignment.CheckinDate =
              con.orderDetails.consignments[0].accommodationData.checkIn;
            this.consignment.CheckoutDate =
              con.orderDetails.consignments[0].accommodationData.checkOut;
            this.consignment.HotelName =
              con.orderDetails.consignments[0].accommodationData.accommodationName;
          }
          //Fields for Transport
          if (this.consignment.ServiceType == 'TRANSPORT') {
            this.consignment.TravellerName =
              con.orderDetails.consignments[0].travellerInfo.travellerName;
            this.consignment.Age =
              con.orderDetails.consignments[0].travellerInfo.travellerAge;
            this.consignment.PassengerType =
              con.orderDetails.consignments[0].travellerInfo.passengerType;
            this.consignment.DepartureDate =
              con.orderDetails.consignments[0].itineraryDetails.departureDateTime;
            this.consignment.ArrivalDate =
              con.orderDetails.consignments[0].itineraryDetails.arrivalDateTime;
            this.consignment.Origin =
              con.orderDetails.consignments[0].itineraryDetails.source;
            this.consignment.Destination =
              con.orderDetails.consignments[0].itineraryDetails.destination;
            this.consignment.Supplier =
              con.orderDetails.consignments[0].itineraryDetails.provider;
          }
        }
      });
  }

  updateStatus(supplierId, orderId, consignmentCode, status) {
    this.consignmentService
      .updateStatus(supplierId, orderId, consignmentCode, status)
      .subscribe((res: any) => {
        this.consignment.ConsignmentStatus = status;
        this.showStatus = true;
        this.router.navigate(['/consignment/' + consignmentCode], {
          queryParams: { isAdmin: true }
        });
        console.log('Successfully updated');
      });
  }
  goBack() {
    // window.history.back();
    this.location.back();

    console.log('goBack()...');
  }
}
