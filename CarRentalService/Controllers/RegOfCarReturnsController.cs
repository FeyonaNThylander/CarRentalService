using CarRentalService.Data;
using CarRentalService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Controllers
{
    public class RegOfCarReturnsController : Controller
    {
        private readonly CarRentalDbContext _context;

        public RegOfCarReturnsController(CarRentalDbContext context)
        {
            _context = context;
        }

        // GET: RegOfCarReturns
        public async Task<IActionResult> Index()
        {
            return _context.RegOfCarReturns != null ?
                        View(await _context.RegOfCarReturns.ToListAsync()) :
                        Problem("Entity set 'CarRentalDbContext.RegOfCarReturns'  is null.");
        }

        public IActionResult ReturnErrorMessage()
        {
            ViewBag.ErrorMessage = "This car has already been returned";
            return View();
        }

        // GET: RegOfCarReturns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RegOfCarReturns == null)
            {
                return NotFound();
            }

            var regOfCarReturn = await _context.RegOfCarReturns.FirstOrDefaultAsync(m => m.BookingNumber == id);
            var regOfCarRental = await _context.RegOfCarRentals.FirstOrDefaultAsync(x => x.BookingNumber == regOfCarReturn.BookingNumber);

            var dailyBaseRentSmallCar = 100;
            var dailyBaseRentStationWagon = 150;
            var dailyBaseRentTruck = 200;

            var baseKmPriceStationWagon = 19.24;
            var baseKmPriceTruck = 20.26;

            var amountOfDaysRented = (regOfCarReturn.DateOfReturn - regOfCarRental.DateOfRental).Days;
            var amountOfKm = regOfCarReturn.CurrentMileage - regOfCarRental.CurrentMileage;

            if (regOfCarRental.CarCategory == "Small car")
            {
                ViewBag.CarCategory = "Small Car";
                ViewBag.CalculatedPrice = (dailyBaseRentSmallCar * amountOfDaysRented);
                ViewBag.AmountOfDaysRented = amountOfDaysRented;
                ViewBag.RentStartDate = regOfCarRental.DateOfRental;
            }
            else if (regOfCarRental.CarCategory == "Station Wagon")
            {
                ViewBag.CarCategory = "Station Wagon";
                double price = Math.Round((dailyBaseRentStationWagon * amountOfDaysRented * 1.3) + (baseKmPriceStationWagon * amountOfKm));
                ViewBag.CalculatedPrice = price;
                ViewBag.AmountOfDaysRented = amountOfDaysRented;
                ViewBag.RentStartDate = regOfCarRental.DateOfRental;
            }
            else if (regOfCarRental.CarCategory == "Truck")
            {
                ViewBag.CarCategory = "Truck";
                double price = Math.Round(((dailyBaseRentTruck * amountOfDaysRented * 1.5) + (baseKmPriceTruck * amountOfKm * 1.5)));
                ViewBag.CalculatedPrice = price;
                ViewBag.AmountOfDaysRented = amountOfDaysRented;
                ViewBag.RentStartDate = regOfCarRental.DateOfRental;
            }

            if (regOfCarReturn == null)
            {
                return NotFound();
            }

            return View(regOfCarReturn);
        }

        // GET: RegOfCarReturns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegOfCarReturns/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingNumber,DateOfReturn,CurrentMileage")] RegOfCarReturn regOfCarReturn)
        {
            if (ModelState.IsValid)
            {
                var bookingNumAmount = await _context.RegOfCarReturns.ToListAsync();
                //var carRentalList = _context.RegOfCarRentals.FirstOrDefault(x => x.BookingNumber == regOfCarReturn.BookingNumber);
                var num = bookingNumAmount.FirstOrDefault(m => m.BookingNumber == regOfCarReturn.BookingNumber);

                if (num == null)
                {
                    _context.Add(regOfCarReturn);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { id = regOfCarReturn.BookingNumber });
                }
            }
            return RedirectToAction("ReturnErrorMessage");
        }
    }
}
