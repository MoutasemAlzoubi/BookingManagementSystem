import { Injectable } from "@angular/core";
import { BookingDTO } from "../Model/BookingDTO";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";

@Injectable({
    providedIn:'root'
})

export class BookingService{
    constructor(private httpClient:HttpClient){

    }

    createBooking(bookingDTO:BookingDTO):Observable<any>{
        return this.httpClient.post('http://localhost/BookingManagementSystem/api/Booking',bookingDTO)
    }


    getTheBooking():Observable<any>{
        return this.httpClient.get('http://localhost/BookingManagementSystem/api/Booking/Getallbooking')

    }


    cancel(id:number):Observable<any>{
        return this.httpClient.delete('http://localhost/BookingManagementSystem/api/Booking?Id='+id)
    }

    searchResourceId(resourceId:number):Observable<any>{
        return this.httpClient.get('http://localhost/BookingManagementSystem/api/Booking/GetBooking?ResourceId='+resourceId)

    }

}