using BookingManagementSystem.Data;
using BookingManagementSystem.Models;

namespace BookingManagementSystem.Service
{
    public interface IBookingService
    {
        Task<BookingDTO> CreateBooking(BookingDTO bookingDTO);

        Task<List<BookingDTO>> GetBookings();

        Task<List<BookingDTO>> GetBooking(int ResourceId);

        Task CancelBooking(int Id);
    }
}


