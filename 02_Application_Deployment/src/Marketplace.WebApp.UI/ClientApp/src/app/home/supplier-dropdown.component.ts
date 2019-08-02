import { Component } from '@angular/core';
import { ConsignmentService } from 'src/api/consignment.service';
import { Router } from '@angular/router';

@Component({
  selector: 'home-supplier-dropdown',
  templateUrl: './supplier-dropdown.component.html',
  styleUrls: ['./home.component.css']
})
export class SupplierDropdownComponent {
  public suppliers: any;

  constructor(
    private consignmentService: ConsignmentService,
    private router: Router
  ) {
    this.suppliers = [];
    if (this.suppliers.length == 0) {
      this.getSuppliersHardCoded();
    }
  }

  getSuppliersHardCoded() {
    let parser: any = {};
    parser.SupplierId = 1;
    parser.SupplierName = 'BHotel';
    this.suppliers.push(parser);

    parser = {};
    parser.SupplierId = 2;
    parser.SupplierName = 'AHotel';
    this.suppliers.push(parser);

    parser = {};
    parser.SupplierId = 3;
    parser.SupplierName = 'Contoso';
    this.suppliers.push(parser);

    parser = {};
    parser.SupplierId = 4;
    parser.SupplierName = 'Car_Rentals';
    this.suppliers.push(parser);
  }

  getSuppliers() {
    this.consignmentService.getVendors().subscribe((res: any[]) => {
      res.forEach(supplier => {
        let parser: any = {};
        parser.SupplierId = supplier.id;
        parser.SupplierName = supplier.name;
        this.suppliers.push(parser);
      });
    });
  }
}
