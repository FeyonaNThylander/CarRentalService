using System.ComponentModel.DataAnnotations;

namespace CarRentalService.Models
{
    public class RegOfCarReturn 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BookingNumber { get; set; }
        [Required]
        public DateTime DateOfReturn { get; set; }
        [Required]
        public int CurrentMileage { get; set; }

    }
}
