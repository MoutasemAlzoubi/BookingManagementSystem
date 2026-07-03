using System.ComponentModel.DataAnnotations;

namespace BookingManagementSystem.Models
{
    public class BookingDTO
    {
        public int? Id { get; set; }

        [Required]
        public int ResourceId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }
    }
}