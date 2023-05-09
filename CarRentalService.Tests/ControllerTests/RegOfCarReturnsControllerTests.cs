using CarRentalService.Controllers;
using CarRentalService.Data;
using CarRentalService.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Tests.ControllerTests
{
    public class RegOfCarReturnsControllerTests
    {
        [Fact]
        public async Task Test_POST_Create_RegCarReturn_RedirectToAction()
        {
            //Arrange
            var newCarReturn = new RegOfCarReturn()
            {
                Id = 22,
                BookingNumber = 400,
                DateOfReturn = DateTime.Now.AddDays(5),
                CurrentMileage = 34567,
            };

            var mock = TestDataHelper.GetFakeCarReturnList().BuildMock().BuildMockDbSet();
            var carContext = new Mock<CarRentalDbContext>();
            carContext.Setup(x => x.RegOfCarReturns).Returns(mock.Object);

            //Act
            RegOfCarReturnsController regOfCarReturnsController = new(carContext.Object);
            var regNewCarReturn = (await regOfCarReturnsController.Create(newCarReturn));

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(regNewCarReturn);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Details", redirectToActionResult.ActionName);

        }

        [Fact]
        public async Task Test_POST_Create_InvalidModelState_RedirectToErrorMessage()
        {
            //Arange
            var newCarReturn = new RegOfCarReturn()
            {
                Id = 11,
                DateOfReturn = DateTime.Now.AddDays(7),
                CurrentMileage = 456456
            };

            var mock = TestDataHelper.GetFakeCarReturnList().BuildMock().BuildMockDbSet();
            var carContext = new Mock<CarRentalDbContext>();
            carContext.Setup(x => x.RegOfCarReturns).Returns(mock.Object);

            //Act
            RegOfCarReturnsController regOfCarReturnsController = new(carContext.Object);
            regOfCarReturnsController.ModelState.AddModelError("BookingNumber", "Required");
            var newCar = await regOfCarReturnsController.Create(newCarReturn);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(newCar);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("ReturnErrorMessage", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Test_GET_Index_ReturnsViewResult_CarReturnsList()
        {
            //Arrange
            var mock = TestDataHelper.GetFakeCarReturnList().BuildMock().BuildMockDbSet();
            var carContext = new Mock<CarRentalDbContext>();
            carContext.Setup(x => x.RegOfCarReturns).Returns(mock.Object);

            //Act
            RegOfCarReturnsController regOfCarReturnsController = new(carContext.Object);
            var cars = await regOfCarReturnsController.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(cars);
            var model = Assert.IsAssignableFrom<IEnumerable<RegOfCarReturn>>(viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }

    }
}
