using BookingManagementSystem.Models;
using BookingManagementSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService bookingService;

        public BookingController(IBookingService _bookingService)
        {
            bookingService = _bookingService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingDTO bookingDTO)
        {
            
                try
                {
                    var result = await bookingService.CreateBooking(bookingDTO);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            
        }

        [HttpGet]
        [Route("Getallbooking")]
        public async Task<IActionResult> GetTheBookings()
        {
            try
            {
                var Result = await bookingService.GetBookings();
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetBooking")]
        public async Task<IActionResult> GetTheBooking(int ResourceId)
        {

            try
            {
                var Result = await bookingService.GetBooking(ResourceId);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }


        [HttpDelete]
        public async Task<IActionResult> CanselTheBooking(int Id)
        {
            try
            {
                await bookingService.CancelBooking(Id);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}




