import { Component, OnInit } from '@angular/core';
import { ConsignmentService } from 'src/api/consignment.service';

@Component({
  selector: 'app-persona',
  templateUrl: './persona.component.html',
  styleUrls: ['./persona.component.css']
})
export class PersonaComponent implements OnInit {

  public suppliers: any;
  constructor( private consignmentService: ConsignmentService) { 
  }

  ngOnInit() {
    this.suppliers = [];
    this.getSuppliersHardCoded();
  }


  getSuppliersHardCoded(){
     
    let parser: any = {};           
    parser.SupplierId = 1;
    parser.SupplierName = "BHotel";
    this.suppliers.push(parser); 
    
    parser = {};
    parser.SupplierId = 2;
    parser.SupplierName = "AHotel";
    this.suppliers.push(parser);  

    parser = {};
    parser.SupplierId = 3;
    parser.SupplierName = "Contoso";
    this.suppliers.push(parser);

    parser = {};
    parser.SupplierId = 4;
    parser.SupplierName = "Car_Rentals";
    this.suppliers.push(parser);
}

  getSuppliers(){
    this.consignmentService.getVendors().subscribe((res : any[])=>{
       res.forEach(supplier => {        
            let parser:any = {};           
            parser.SupplierId = supplier.id;
            parser.SupplierName = supplier.name;
            this.suppliers.push(parser);
       });   
   });    
  }

}
