using System;
using System.Threading.Tasks;
using CarRentalSystem.DTOs;
using CarRentalSystem.Models;
using CarRentalSystem.Repositories.Interface;
using CarRentalSystem.Services.Implementation;
using Moq;
using Xunit;
namespace CarRentalSystem.Tests
{
    public class RentalServiceTests
    {
        [Fact]
        public void CalculateRentalPrice_ShouldCalculateCorrectPrice()
        {
            
            var mockCarRepository = new Mock<ICarRepository>();
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var mockRentalRepository = new Mock<IRentalRepository>();

            var rentalService = new RentalService(mockCarRepository.Object, mockCustomerRepository.Object, mockRentalRepository.Object);

            var rentCarRequest = new RentCarRequest
            {
                CarId = 1,
                DurationDays = 5
            };

            var carInDb = new Car { Id = 1, Type = CarType.Premium };
            mockCarRepository.Setup(r => r.GetById(rentCarRequest.CarId)).Returns(carInDb);

            
            var totalPrice = rentalService.CalculateRentalPrice(rentCarRequest);

            
            Assert.Equal(1500, totalPrice); // Assuming Premium Price is 300 and Duration is 5 days
        }

        [Fact]
        public async Task RentCar_ShouldRentCarAndAddRental()
        {
            
            var mockCarRepository = new Mock<ICarRepository>();
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var mockRentalRepository = new Mock<IRentalRepository>();

            var rentalService = new RentalService(mockCarRepository.Object, mockCustomerRepository.Object, mockRentalRepository.Object);

            var rentCarRequest = new RentCarRequest
            {
                CarId = 1,
                StartDate = DateTime.Today,
                DurationDays = 3,
                CustomerId = 1
            };

            var carInDb = new Car { Id = 1, IsRented = false };
            var customerInDb = new Customer { Id = 1, LoyaltyPoints = 0 };

            mockCarRepository.Setup(r => r.GetById(rentCarRequest.CarId)).Returns(carInDb);
            mockCustomerRepository.Setup(c => c.GetById(rentCarRequest.CustomerId)).Returns(customerInDb);

            
            await rentalService.RentCar(rentCarRequest);

            
            Assert.True(carInDb.IsRented);
            mockCarRepository.Verify(r => r.UpdateCar(carInDb), Times.Once);
            mockRentalRepository.Verify(r => r.AddRental(It.IsAny<Rental>()), Times.Once);
            Assert.Equal(5, customerInDb.LoyaltyPoints); // Assuming Premium car gives 5 loyalty points
            mockCustomerRepository.Verify(c => c.UpdateCustomer(customerInDb), Times.Once);
        }

        [Fact]
        public async Task ReturnCar_ShouldReturnCarAndUpdateRental()
        {
            
            var mockCarRepository = new Mock<ICarRepository>();
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var mockRentalRepository = new Mock<IRentalRepository>();

            var rentalService = new RentalService(mockCarRepository.Object, mockCustomerRepository.Object, mockRentalRepository.Object);

            var returnCarRequest = new ReturnCarRequest
            {
                RentalId = 1,
                ReturnDate = DateTime.Today
            };

            var rentalInDb = new Rental { Id = 1, CarId = 1, StartDate = DateTime.Today.AddDays(-7), DurationDays = 5, Car = new Car { Id = 1 } };
            var carInDb = new Car { Id = 1, IsRented = true };

            mockRentalRepository.Setup(r => r.GetById(returnCarRequest.RentalId)).Returns(rentalInDb);
            mockCarRepository.Setup(r => r.GetById(rentalInDb.CarId)).Returns(carInDb);

            
            var extraCharges = await rentalService.ReturnCar(returnCarRequest);

            
            Assert.False(carInDb.IsRented);
            mockCarRepository.Verify(r => r.UpdateCar(carInDb), Times.Once);
            Assert.Equal(720, extraCharges);
            rentalInDb.ReturnDate = DateTime.Today;
            mockRentalRepository.Verify(r => r.UpdateRental(rentalInDb), Times.Once);
        }
    }

}
