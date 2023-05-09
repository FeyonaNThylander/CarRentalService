using CarRentalService.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Data
{
    public class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext(DbContextOptions options) : base(options)
        {
        }

        public CarRentalDbContext()
        {
        }

        public virtual DbSet<RegOfCarRental> RegOfCarRentals { get; set; } 
        public virtual DbSet<RegOfCarReturn> RegOfCarReturns { get; set; } 

    }
}
