import { Injectable } from '@angular/core';
import { IConsignment } from '../app/consignment';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { IOrder } from './models/createOrder';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';

@Injectable({
    providedIn: 'root'
})
export class ConsignmentService {

    private serviceUrl;
    private adminData = [];

    constructor(private httpClient: HttpClient){
        this.serviceUrl = "http://blockchain-marketplace.eastus.cloudapp.azure.com:9055/api";   
    }

    
    getVendors(): any {
        this.serviceUrl = "http://blockchain-marketplace.eastus.cloudapp.azure.com:9055/api";
        let path = "/Order/Vendors/All";
        return this.httpClient.get(this.serviceUrl + path);
    }


    getAdminData(): any {
        this.serviceUrl = "http://blockchain-marketplace.eastus.cloudapp.azure.com:9055/api";
        let path = "/Admin";
        return this.httpClient.get(this.serviceUrl + path);
    }

    getConsignmentByID(consignmentCode) {
        this.serviceUrl = "http://blockchain-marketplace.eastus.cloudapp.azure.com:9055/api";
        //this.serviceUrl = './assets/adminData.json';
        let path = "/Order/Consignment/" + consignmentCode;
        return this.httpClient.get(this.serviceUrl + path);
    }

    getSupplierData(supplierId): any {
        this.serviceUrl = "http://blockchain-marketplace.eastus.cloudapp.azure.com:9055/api";
        //this.serviceUrl = './assets/adminData.json';
        let path = "/Supplier?supplierId=" + supplierId;
        return this.httpClient.get(this.serviceUrl + path);
    }

    postOrder(orderData: IOrder): Observable<Object> {
        this.serviceUrl = "http://blockchain-marketplace.eastus.cloudapp.azure.com:9055/api";
        let path = "/Order"
        return this.httpClient.post(this.serviceUrl + path, orderData);
        //return of(orderData);
    }

    updateStatus(supplierId, orderId, consignmentCode, status): any{
        let path = `/Supplier?supplierId=${supplierId}&orderID=${orderId}&consignmentCode=${consignmentCode}&status=${status}`
        return this.httpClient.put(this.serviceUrl + path, null);
    }
    // getConsignments() : IConsignment[] {
    //     let consignment = [{
    //         "OrderCode"      : "EGSF7",
    //         "ConsignmentCode"      : "ABC100",  
    //         "OrderStatus" : "ACTIVE",
    //         "ConsignmentStatus" : "ACTIVE",
    //         "BookingDate" : new Date("2018-10-25"), 
    //         "ServiceType" : "ACTIVITY",
    //         "ServiceDate" : new Date("2018-11-30"),
    //         "Supplier"      : "ArabianAdventure",
    //         "CheckinDate" : new Date("2018-11-30"),
    //         "CheckoutDate" : new Date("2018-11-30"),
    //         "Quantity"      : 1,
    //         "Price"          : 5147,
    //         "Currency"      : "USD"
    //     },
    //     {
    //         "OrderCode"      : "EGSF7",
    //         "ConsignmentCode"      : "ABC101",  
    //         "OrderStatus" : "ACTIVE",
    //         "ConsignmentStatus" : "ACTIVE",
    //         "BookingDate" : new Date("2018-10-25"), 
    //         "ServiceType" : "ACTIVITY",
    //         "ServiceDate" : new Date("2018-11-30"),
    //         "Supplier"      : "ArabianAdventure",
    //         "CheckinDate" : new Date("2018-11-30"),
    //         "CheckoutDate" : new Date("2018-11-30"),
    //         "Quantity"      : 1,
    //         "Price"          : 5147,
    //         "Currency"      : "USD"
    //     },
    //     {
    //         "OrderCode"      : "EGSF7",
    //         "ConsignmentCode"      : "ABC102",  
    //         "OrderStatus" : "ACTIVE",
    //         "ConsignmentStatus" : "ACTIVE",
    //         "BookingDate" : new Date("2018-10-25"), 
    //         "ServiceType" : "ACTIVITY",
    //         "ServiceDate" : new Date("2018-11-30"),
    //         "Supplier"      : "ArabianAdventure",
    //         "CheckinDate" : new Date("2018-11-30"),
    //         "CheckoutDate" : new Date("2018-11-30"),
    //         "Quantity"      : 1,
    //         "Price"          : 5147,
    //         "Currency"      : "USD"
    //     },
    //     {
    //         "OrderCode"      : "XYZ124",
    //         "ConsignmentCode"      : "ABC100",  
    //         "OrderStatus" : "ACTIVE",
    //         "ConsignmentStatus" : "ACTIVE",
    //         "BookingDate" : new Date("2018-10-25"), 
    //         "ServiceType" : "ACTIVITY",
    //         "ServiceDate" : new Date("2018-11-30"),
    //         "Supplier"      : "ArabianAdventure",
    //         "CheckinDate" : new Date("2018-11-30"),
    //         "CheckoutDate" : new Date("2018-11-30"),
    //         "Quantity"      : 1,
    //         "Price"          : 5147,
    //         "Currency"      : "USD"
    //     }
    //     ]
    //     return consignment;
    // }

    // getSupplierConsignments(supplierCode) : IConsignment[] {
    //     let supplierConsignments = [{
    //             "OrderCode"      : "EGSF7",
    //             "ConsignmentCode"      : "ABC100",  
    //             "OrderStatus" : "ACTIVE",
    //             "ConsignmentStatus" : "ACTIVE",
    //             "BookingDate" : new Date("2018-10-25"), 
    //             "ServiceType" : "ACTIVITY",
    //             "ServiceDate" : new Date("2018-11-30"),
    //             "Supplier"      : "ArabianAdventure",
    //             "CheckinDate" : new Date("2018-11-30"),
    //             "CheckoutDate" : new Date("2018-11-30"),
    //             "Quantity"      : 1,
    //             "Price"          : 5147,
    //             "Currency"      : "USD"
    //         },
    //         {
    //             "OrderCode"      : "EGSF7",
    //             "ConsignmentCode"      : "ABC101",  
    //             "OrderStatus" : "ACTIVE",
    //             "ConsignmentStatus" : "ACTIVE",
    //             "BookingDate" : new Date("2018-10-25"), 
    //             "ServiceType" : "ACTIVITY",
    //             "ServiceDate" : new Date("2018-11-30"),
    //             "Supplier"      : "ArabianAdventure",
    //             "CheckinDate" : new Date("2018-11-30"),
    //             "CheckoutDate" : new Date("2018-11-30"),
    //             "Quantity"      : 1,
    //             "Price"          : 5147,
    //             "Currency"      : "USD"
    //         },
    //         {
    //             "OrderCode"      : "EGSF7",
    //             "ConsignmentCode"      : "ABC102",  
    //             "OrderStatus" : "ACTIVE",
    //             "ConsignmentStatus" : "ACTIVE",
    //             "BookingDate" : new Date("2018-10-25"), 
    //             "ServiceType" : "ACTIVITY",
    //             "ServiceDate" : new Date("2018-11-30"),
    //             "Supplier"      : "ArabianAdventure",
    //             "CheckinDate" : new Date("2018-11-30"),
    //             "CheckoutDate" : new Date("2018-11-30"),
    //             "Quantity"      : 1,
    //             "Price"          : 5147,
    //             "Currency"      : "USD"
    //         },
    //         {
    //             "OrderCode"      : "XYZ124",
    //             "ConsignmentCode"      : "ABC100",  
    //             "OrderStatus" : "ACTIVE",
    //             "ConsignmentStatus" : "ACTIVE",
    //             "BookingDate" : new Date("2018-10-25"), 
    //             "ServiceType" : "ACTIVITY",
    //             "ServiceDate" : new Date("2018-11-30"),
    //             "Supplier"      : "ArabianAdventure",
    //             "CheckinDate" : new Date("2018-11-30"),
    //             "CheckoutDate" : new Date("2018-11-30"),
    //             "Quantity"      : 1,
    //             "Price"          : 5147,
    //             "Currency"      : "USD"
    //         }
    //     ]
    //     const result = supplierConsignments.filter(c => c.Supplier == supplierCode);
    //     return result;
    // }

 
}