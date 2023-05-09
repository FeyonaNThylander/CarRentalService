using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalService.Models
{
    public class RegOfCarRental
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BookingNumber { get; set; }
        [Required]
        public string RegistrationNumber { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string CarCategory { get; set; }
        [Required]
        public DateTime DateOfRental { get; set; }
        [Required]
        public int CurrentMileage { get; set; }

    }
}
