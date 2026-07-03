import { Component, OnInit } from '@angular/core';
import { BookingService } from '../Service/BookingService';
import { BookingDTO } from '../Model/BookingDTO';

@Component({
  selector: 'app-booking-list',
  templateUrl: './booking-list.component.html',
  styleUrls: ['./booking-list.component.css']
})
export class BookingListComponent implements OnInit {
  allBooking!:BookingDTO[]
  resourceId!:number

  constructor(private bookingService:BookingService){

  }


  ngOnInit(): void {
    if(this.resourceId == null){
    this.getAllBooking()
    }
  }


  getAllBooking(){
    

    this.bookingService.getTheBooking().subscribe({
      next:data=>{
        this.allBooking = data
        // this.allPatientsBackup = data;
        console.log("Booking brought successfuly")
      },
      error:()=>{
        console.log("error happened")
      }
    })

  }
    
  

  cancel(id:number){
    
    this.bookingService.cancel(id).subscribe({
      next:()=>{
        console.log("Booking Cancelled")
        this.getAllBooking()
      },
      error:()=>{
        console.log("error happened")
      }
    })

  }




  searchByName(){

  // if(this.resourceId == null){
  //   this.getAllBooking()

  // }
  
  this.bookingService.searchResourceId(this.resourceId).subscribe({
    next:(data)=>{
      this.allBooking = data
      
      console.log('Search successfull')
    },
    error:()=>{
      console.log("error happend")
    }
  })
}

}
