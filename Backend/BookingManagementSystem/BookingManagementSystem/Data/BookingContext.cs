using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Data
{
    public class BookingContext : DbContext
    {
        private readonly IConfiguration configuration;

        public BookingContext(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        
        public BookingContext(DbContextOptions<BookingContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("bookingConn"));
            }
        }
    }
}