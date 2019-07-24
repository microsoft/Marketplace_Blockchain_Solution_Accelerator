import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminViewComponent } from './admin-view/admin-view.component';
import { HomeComponent } from './home/home.component';
import { CreateConsignmentComponent } from './create-consignment/create-consignment.component';
import { SupplierViewComponent } from './supplier-view/supplier-view.component';
import { ViewConsignmentComponent } from './view-consignment/view-consignment.component';
import { PersonaComponent } from './persona/persona.component';

const routes: Routes = [
  { path: '', component: PersonaComponent },
  { path: 'admin', component: AdminViewComponent },
  { path: 'supplier/:id', component: SupplierViewComponent }, 
  { path: 'consignment/:id', component: ViewConsignmentComponent },
  { path: 'create', component: CreateConsignmentComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
