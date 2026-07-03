using BookingManagementSystem.Data;
using BookingManagementSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookingManagementSystem.Service
{
    public class BookingService : IBookingService
    {
        private readonly BookingContext context;

        public BookingService(BookingContext _context)
        {
            context = _context;
        }

        public async Task<BookingDTO> CreateBooking(BookingDTO bookingDTO)
        {
            if (bookingDTO.StartDateTime >= bookingDTO.EndDateTime)
                throw new Exception("Invalid time range");



            //<<----------OverLapCheck--------------->> ;)

            var Overlap = await context.bookings.AnyAsync(b => b.ResourceId == bookingDTO.ResourceId &&
                                                              b.Status == "Active" &&
                                                              b.StartDateTime < bookingDTO.EndDateTime &&
                                                              b.EndDateTime > bookingDTO.StartDateTime);
            if (Overlap == true)
                throw new Exception("There is a booking in this time range");

            Booking booking = new Booking();
            booking.ResourceId = bookingDTO.ResourceId;
            booking.Status = "Active";
            booking.UserId = bookingDTO.UserId;
            booking.StartDateTime = bookingDTO.StartDateTime;
            booking.EndDateTime = bookingDTO.EndDateTime;
            booking.CreatedAt = DateTime.UtcNow;
            await context.bookings.AddAsync(booking);
            await context.SaveChangesAsync();


            return bookingDTO;







        }




       public async Task<List<BookingDTO>> GetBookings()
        {
            List<Booking> bookings = await context.bookings.Where(b => b.Status == "Active").ToListAsync();
            List<BookingDTO> bookingDTOs = new List<BookingDTO>();
            foreach (Booking booking in bookings)
            {
                BookingDTO bookingDTO = new BookingDTO();
                bookingDTO.Id = booking.Id;
                bookingDTO.UserId = booking.UserId;
                bookingDTO.ResourceId = booking.ResourceId;
                bookingDTO.StartDateTime = booking.StartDateTime;
                bookingDTO.EndDateTime = booking.EndDateTime;
                bookingDTOs.Add(bookingDTO);

            }
            return bookingDTOs;

        }

        public async Task<List<BookingDTO>> GetBooking(int ResourceId)
        {
            List<Booking> bookings = await context.bookings.Where(b => b.UserId == ResourceId || 
                                                                  b.ResourceId == ResourceId).ToListAsync();
            List<BookingDTO> bookingDTOs = new List<BookingDTO>();
            foreach (Booking booking in bookings)
            {
                BookingDTO bookingDTO = new BookingDTO();
                bookingDTO.Id = booking.Id;
                bookingDTO.UserId = booking.UserId;
                bookingDTO.ResourceId = booking.ResourceId;
                bookingDTO.StartDateTime = booking.StartDateTime;
                bookingDTO.EndDateTime = booking.EndDateTime;
                bookingDTOs.Add(bookingDTO);

            }
            return bookingDTOs;


        }




        public async Task CancelBooking(int Id)
        {
            Booking booking = await context.bookings.FindAsync(Id);

            if (booking == null)
                throw new Exception("Booking not found");

            if (booking.Status == "Cancelled")
                throw new Exception("Booking is cancelled");

            booking.Status = "Cancelled";
            booking.CancelledAt = DateTime.UtcNow;

            await context.SaveChangesAsync();

            
        }






    }
}
