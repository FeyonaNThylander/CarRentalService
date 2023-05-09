using CarRentalService.Controllers;
using CarRentalService.Data;
using CarRentalService.Models;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;

namespace CarRentalService.Tests.ControllerTests
{
    public class RegOfCarRentalsControllerTests
    {
        [Fact]
        public async Task Test_POST_Create_RegCarRental_RedirectToAction()
        {
            //Arrange
            var newCarRental = new RegOfCarRental()
            {
                Id = 11,
                BookingNumber = 400,
                RegistrationNumber = "OLK888",
                CustomerId = "870908",
                CarCategory = "Small car",
                DateOfRental = DateTime.Now,
                CurrentMileage = 23456,
            };

            var mock = TestDataHelper.GetFakeCarRentalList().BuildMock().BuildMockDbSet();
            var carContext = new Mock<CarRentalDbContext>();
            carContext.Setup(x => x.RegOfCarRentals).Returns(mock.Object);

            //Act
            RegOfCarRentalsController regOfCarRentalsController = new(carContext.Object);
            var newCar = await regOfCarRentalsController.Create(newCarRental);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(newCar);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Details", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Test_POST_Create_InvalidModelState_RedirectToErrorMessage()
        {
            //Arange
            var newCarRental = new RegOfCarRental()
            {
                Id = 11,
                RegistrationNumber = "OLK888",
                CustomerId = "870908",
                CarCategory = "Small car",
                DateOfRental = DateTime.Now,
                CurrentMileage = 23456,
            };

            var mock = TestDataHelper.GetFakeCarRentalList().BuildMock().BuildMockDbSet();
            var carContext = new Mock<CarRentalDbContext>();
            carContext.Setup(x => x.RegOfCarRentals).Returns(mock.Object);

            //Act
            RegOfCarRentalsController regOfCarRentalsController = new(carContext.Object);
            regOfCarRentalsController.ModelState.AddModelError("BookingNumber", "Required");
            var newCar = await regOfCarRentalsController.Create(newCarRental);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(newCar);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("RentalErrorMessage", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Test_GET_Index_ReturnsViewResult_CarRentalsList()
        {
            //Arrange
            var mock = TestDataHelper.GetFakeCarRentalList().BuildMock().BuildMockDbSet();
            var carContext = new Mock<CarRentalDbContext>();
            carContext.Setup(x => x.RegOfCarRentals).Returns(mock.Object);

            //Act
            RegOfCarRentalsController regOfCarRentalsController = new(carContext.Object);
            var cars = await regOfCarRentalsController.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(cars);
            var model = Assert.IsAssignableFrom<IEnumerable<RegOfCarRental>>(viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public async Task Test_GET_DetailsWithId_ReturnsViewResult_WithSingleCarRental()
        {
            //Arrange
            var mock = TestDataHelper.GetFakeCarRentalList().BuildMock().BuildMockDbSet();
            mock.Setup(x => x.FindAsync(100)).ReturnsAsync(
                TestDataHelper.GetFakeCarRentalList().Find(a => a.BookingNumber == 100));

            var carContext = new Mock<CarRentalDbContext>();
            carContext.Setup(e => e.RegOfCarRentals).Returns(mock.Object);

            //Act
            RegOfCarRentalsController regOfCarRentalsController = new(carContext.Object);
            var car = await regOfCarRentalsController.Details(100);

            //Assert
            Assert.NotNull(car);
            var viewResult = Assert.IsType<ViewResult>(car);
            var model = Assert.IsAssignableFrom<RegOfCarRental>(viewResult.ViewData.Model);
            Assert.Equal(100, model.BookingNumber);
        }

    }
}
