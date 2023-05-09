using CarRentalService.Data;
using CarRentalService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Controllers
{
    public class RegOfCarRentalsController : Controller
    {
        private readonly CarRentalDbContext _context;

        public RegOfCarRentalsController(CarRentalDbContext context)
        {
            _context = context;
        }

        // GET: RegOfCarRentals
        public async Task<IActionResult> Index()
        {
            var carRentalsList = await _context.RegOfCarRentals.ToListAsync();
            return View(carRentalsList);
        }

        public IActionResult RentalErrorMessage()
        {
            ViewBag.ErrorMessage = "We're sorry but the car is unavailable. Please choose another one.";
            return View();
        }

        // GET: RegOfCarRentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var regOfCarRental = await _context.RegOfCarRentals.FirstOrDefaultAsync(m => m.BookingNumber == id);

            if (id == null || _context.RegOfCarRentals == null || regOfCarRental == null)
            {
                return NotFound();
            }
            return View(regOfCarRental);
        }

        // GET: RegOfCarRentals/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CarCategories = new List<string>() { "Small car", "Station Wagon", "Truck" };
            return View();
        }

        // POST: RegOfCarRentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingNumber,RegistrationNumber,CustomerId,CarCategory,DateOfRental,CurrentMileage")] RegOfCarRental regOfCarRental)
        {
            var regBookingNum = await _context.RegOfCarRentals.FirstOrDefaultAsync(a => a.BookingNumber == regOfCarRental.BookingNumber);
            var carIsBusy = await _context.RegOfCarRentals.FirstOrDefaultAsync(x => x.RegistrationNumber == regOfCarRental.RegistrationNumber);

            if (ModelState.IsValid)
            {
                if (regBookingNum == null && carIsBusy == null)
                {
                    _context.Add(regOfCarRental);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { id = regOfCarRental.BookingNumber });
                }
            }
            return RedirectToAction("RentalErrorMessage");
        }

    }
}
