import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NewBookingComponent } from './new-booking/new-booking.component';
import { BookingListComponent } from './booking-list/booking-list.component';

const routes: Routes = [
  {path:'',component:HomeComponent},
  {path:'NewBooking',component:NewBookingComponent},
  {path:'BookingList',component:BookingListComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
