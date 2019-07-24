import { Component, OnInit } from '@angular/core';
import { IConsignment } from '../consignment';
import { ConsignmentService } from '../../api/consignment.service';
import { ActivatedRoute } from '@angular/router';
import { AdminConsignment } from 'src/api/models/adminConsignment';

@Component({
  selector: 'app-supplier-view',
  templateUrl: './supplier-view.component.html',
  styleUrls: ['./supplier-view.component.css']
})
export class SupplierViewComponent implements OnInit {

  consignments : IConsignment[];
  dataForAdmin: any[];
  supplierCode: string;
  totalPrice : number = 0;
  currentPage: number;
  page : number = 1;
  pageSize: number = 10
  constructor(private consignmentService: ConsignmentService, private route: ActivatedRoute) { 

  }
 
  public barChartOptions = {
    title: {
      text: 'Consignment Summary for 2019',
      display: true
    },
    scaleShowVerticalLines: false,
    responsive: true,
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
    {data: [], label: 'New'},
    {data: [], label: 'In Progress'},
    {data: [], label: 'Approved'},
    {data: [], label: 'Rejected'}
  ];

  ngOnInit() {
    this.supplierCode = this.route.snapshot.paramMap.get('id');
    this.get_SupplierData();
    this.dataForAdmin = [];
    //get the consignments for the supplier.
    //this.consignments  = this.consignmentService.getSupplierConsignments(supplierCode);
  }

  get_SupplierData(){
   this.consignmentService.getAdminData().subscribe((res : any[])=>{
     this.dataForAdmin = [];
     let sum: number = 0;
      res = res.filter(x => x.businessContractDTO.adminDetail.supplier == this.supplierCode);
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

  changePage(pageNumber){
    console.log(pageNumber);
  }
}
