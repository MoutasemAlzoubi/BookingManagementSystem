import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BookingDTO } from '../Model/BookingDTO';
import { BookingService } from '../Service/BookingService';

@Component({
  selector: 'app-new-booking',
  templateUrl: './new-booking.component.html',
  styleUrls: ['./new-booking.component.css']
})
export class NewBookingComponent implements OnInit {

  constructor (private formBuilder:FormBuilder,
               private bookingService:BookingService
  ){

  }
  bookingForm!:FormGroup

  ngOnInit(): void {
    this.builedTheForm()

    
  }
  builedTheForm(){
  
    this.bookingForm = this.formBuilder.group({
      txtResource:['',Validators.required],
      txtUserId:['',Validators.required],
      txtStartDate:['',Validators.required],
      txtEndDate:['',Validators.required],


    })
    
  }





  create(){


    if(this.bookingForm.valid){
      var bookingDTO = new BookingDTO()
      
      bookingDTO.resourceId = this.bookingForm.value['txtResource']
      bookingDTO.userId = this.bookingForm.value['txtUserId']
      bookingDTO.startDateTime = this.bookingForm.value['txtStartDate']
      bookingDTO.endDateTime = this.bookingForm.value['txtEndDate']

      this.bookingService.createBooking(bookingDTO).subscribe({
        next:()=>{
          console.log("Booking saved successfully")
        },
        error:()=>{
          console.log("error happened")
        }
      })
    }

  }


  






}
