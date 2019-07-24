import { Component, OnInit } from '@angular/core';
import { ConsignmentService } from '../../api/consignment.service';
import { IConsignment } from '../consignment';
import { AdminConsignment } from 'src/api/models/adminConsignment';
import { ActivatedRoute, Router } from '@angular/router';
//import {map } from 'rxjs/operators/map';
//import 'rxjs/add/operator/switchMap';

@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.css']
})

export class AdminViewComponent implements OnInit {
  config: any; 
  collection: [];
  dataForAdmin: any[];
  consignments : IConsignment[];
  totalPrice : number = 0;
  currentPage: number;
  page : number = 1;
  pageSize: number = 10

  constructor(private consignmentService: ConsignmentService, private route: ActivatedRoute, private router: Router) { 

    this.dataForAdmin = [];
    this.get_AdminData();

    // this.config = {
    //   id: 'page-listing',
    //   itemsPerPage: 2,
    //   currentPage: this.p,
    //   totalItems: this.dataForAdmin.length
    // };
  }

  public barChartOptions = {
    title: {
      text: 'Consignment Summary for 2019',
      display: true
    },
    scaleShowVerticalLines: false,
   // responsive: true,
    scales: {
      yAxes: [{
        scaleLabel: {
          display: true,
          labelString: 'Order Count'
        }
      }],
      xAxes: [{
        scaleLabel: {
          display: true,
          labelString: 'TimeStamp'
        }
      }]
    }
  };

  public barChartLabels = ['Jan', 'Feb', 'March', 'April', 'May', 'June', 'July', 'August', 'Sep', 'Oct', 'Nov', 'Dec'];
  public barChartType = 'line';
  public barChartLegend = true;
  public barChartData = [
    {data: [], label: 'READY'},
    {data: [], label: 'IN PROGRESS'},
    {data: [], label: 'ACTIVE'},
    {data: [], label: 'REJECTED'}
  ];

  ngOnInit() {  
   
   // this.config.totalItems = this.dataForAdmin.length;
  }

  get_AdminData(){
    this.consignmentService.getAdminData().subscribe((res : any[])=>{
    this.dataForAdmin = [];
    let sum: number = 0;
    res = res.reverse();
       res.forEach(contract => {
            let parser = new AdminConsignment();
            let adminFields = contract.businessContractDTO.adminDetail;           
            parser.BindingId = contract.bindingId;
            parser.OrderCode = adminFields.orderCode;
            parser.ConsignmentCode = adminFields.consignmentCode.substring(0,14);
            parser.ConsignmentStatus = adminFields.consignmentStatus;
            parser.ServiceType = adminFields.serviceType;
            parser.ServiceDate = adminFields.serviceDate;
            parser.Supplier = adminFields.supplier;
            parser.Currency = adminFields.currency;
            parser.BookingDate = adminFields.bookingDate;
            parser.CheckinDate = adminFields.checkinDate? adminFields.checkinDate : 'N/A';
            parser.Price = Number(adminFields.price);
            parser.Quantity = adminFields.quantity;
            sum = sum + parser.Price;
            this.dataForAdmin.push(parser);
       });
       this.totalPrice = sum;
       this.createChart();
   });    
  }

  createChart(){
    let uniqueStates = new Set();
    //find all the unique states of consignments
    if(this.dataForAdmin.length > 0){
        this.dataForAdmin.forEach((di:any) => {
            uniqueStates.add(di.ConsignmentStatus);
        })   
    }
    //find data for each month and aggregate by that
    var chartData = [];
    uniqueStates.forEach((state:any)=> {
        let obj:any = {};
        obj.data = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
        this.dataForAdmin.forEach(item =>{
          if(state == item.ConsignmentStatus){
            var a = Date.parse(item.ServiceDate);
            if(!isNaN(a))
            {
                var dateTemp = new Date(a);
                let index: number = dateTemp.getMonth();
                obj.data[index]++;
            }
          }
        })
        obj.label = state;
        chartData.push(obj);
    });
    this.barChartData = chartData;
  }
 
  isValidDate(date) {
    return date && Object.prototype.toString.call(date) === "[object Date]" && !isNaN(date);
  }

  changePage(pageNumber){
    console.log(pageNumber);
  }
}
