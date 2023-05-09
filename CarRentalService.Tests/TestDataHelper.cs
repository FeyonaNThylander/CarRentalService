using CarRentalService.Models;

namespace CarRentalService.Tests
{
    public class TestDataHelper
    {
        public static List<RegOfCarRental> GetFakeCarRentalList()
        {
            return new List<RegOfCarRental>()
            {
              new RegOfCarRental
              {
                  Id = 6,
                  BookingNumber = 100,
                  RegistrationNumber = "JKL999",
                  CustomerId = "861230",
                  CarCategory = "Small car",
                  DateOfRental = DateTime.Now,
                  CurrentMileage = 45678,
              },
              new RegOfCarRental
              {
                  Id = 7,
                  BookingNumber = 200,
                  RegistrationNumber = "DFG111",
                  CustomerId = "871022",
                  CarCategory = "Station Wagon",
                  DateOfRental = DateTime.Now,
                  CurrentMileage = 67899,
              },
              new RegOfCarRental
              {
                  Id = 8,
                  BookingNumber = 300,
                  RegistrationNumber = "WER333",
                  CustomerId = "871022",
                  CarCategory = "Truck",
                  DateOfRental = DateTime.Now,
                  CurrentMileage = 67899,
              }
            };
        }

        public static List<RegOfCarReturn> GetFakeCarReturnList()
        {
            return new List<RegOfCarReturn>()
            {
                new RegOfCarReturn
                {
                    Id = 1,
                    BookingNumber= 100,
                    DateOfReturn = DateTime.Now.AddDays(12),
                    CurrentMileage = 56789,
                },
                new RegOfCarReturn
                {
                    Id = 2,
                    BookingNumber= 200,
                    DateOfReturn = DateTime.Now.AddDays(3),
                    CurrentMileage = 67890,
                },
                new RegOfCarReturn
                {
                    Id = 3,
                    BookingNumber= 300,
                    DateOfReturn = DateTime.Now.AddDays(7),
                    CurrentMileage = 69876,
                }
            };
        }
    }
}
