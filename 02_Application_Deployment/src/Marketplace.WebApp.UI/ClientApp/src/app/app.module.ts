import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavbarComponent } from './common/navbar/navbar.component';
import { SupplierDropdownComponent } from './home/supplier-dropdown.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { AdminViewComponent } from './admin-view/admin-view.component';
import { SupplierViewComponent } from './supplier-view/supplier-view.component';
import { CreateConsignmentComponent } from './create-consignment/create-consignment.component';
import { ViewConsignmentComponent } from './view-consignment/view-consignment.component'
import { SidebarComponent } from './common/sidebar/sidebar.component';
import { ChartsModule } from 'ng2-charts';
import { HttpClientModule } from '@angular/common/http';
// import { NgxPaginationModule } from 'ngx-pagination';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { PersonaComponent } from './persona/persona.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    SupplierDropdownComponent,
    AdminViewComponent,
    SupplierViewComponent,
    CreateConsignmentComponent,
    ViewConsignmentComponent,
    SidebarComponent,
    PersonaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BsDropdownModule.forRoot(),
    ChartsModule,
    BsDatepickerModule.forRoot(),
    FormsModule,
    HttpClientModule,
    NgbPaginationModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
