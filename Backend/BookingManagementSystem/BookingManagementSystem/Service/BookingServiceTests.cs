using BookingManagementSystem.Data;
using BookingManagementSystem.Models;
using BookingManagementSystem.Service;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BookingManagementSystem.Tests
{
    public class BookingServiceTests
    {
        private BookingContext GetContext()
        {
            var options = new DbContextOptionsBuilder<BookingContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new BookingContext(options);
        }

        private BookingService GetService(BookingContext context)
        {
            return new BookingService(context);
        }

        [Fact]
        public async Task CreateBooking_Should_Succeed()
        {
            var context = GetContext();
            var service = GetService(context);

            var dto = new BookingDTO
            {
                ResourceId = 1,
                UserId = 1,
                StartDateTime = DateTime.UtcNow.AddHours(1),
                EndDateTime = DateTime.UtcNow.AddHours(2)
            };

            var result = await service.CreateBooking(dto);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateBooking_Should_Fail_When_Overlap()
        {
            var context = GetContext();
            var service = GetService(context);

            await service.CreateBooking(new BookingDTO
            {
                ResourceId = 1,
                UserId = 1,
                StartDateTime = DateTime.UtcNow.AddHours(1),
                EndDateTime = DateTime.UtcNow.AddHours(3)
            });

            await Assert.ThrowsAsync<Exception>(() =>
                service.CreateBooking(new BookingDTO
                {
                    ResourceId = 1,
                    UserId = 2,
                    StartDateTime = DateTime.UtcNow.AddHours(2),
                    EndDateTime = DateTime.UtcNow.AddHours(4)
                }));
        }

        [Fact]
        public async Task CancelBooking_Should_Work()
        {
            var context = GetContext();
            var service = GetService(context);

            await service.CreateBooking(new BookingDTO
            {
                ResourceId = 1,
                UserId = 1,
                StartDateTime = DateTime.UtcNow.AddHours(1),
                EndDateTime = DateTime.UtcNow.AddHours(2)
            });

            var booking = context.bookings.First();

            await service.CancelBooking(booking.Id);

            Assert.Equal("Cancelled", context.bookings.First().Status);
        }
    }
}