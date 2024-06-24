using CarRentalSystem.Models;
using CarRentalSystem.Repositories.Interface;
using CarRentalSystem.Services.Implementation;
using Moq;

namespace CarRentalSystem.Tests
{
    public class CarServiceTests
    {
        [Fact]
        public void GetAll_ShouldReturnAllCars()
        {

            var mockRepository = new Mock<ICarRepository>();
            var cars = new List<Car>
            {
                new() { Id = 1, Model = "BMW 7", Type = CarType.Premium },
                new () { Id = 2, Model = "Kia Sorento", Type = CarType.Suv }
        };
            mockRepository.Setup(r => r.GetAll()).Returns(cars);

            var carService = new CarService(mockRepository.Object);

            var result = carService.GetAll();

            Assert.NotNull(result);
            Assert.Equal(cars.Count, result.Count());
            Assert.Equal(cars, result.ToList());
        }

        [Fact]
        public void GetById_ShouldReturnCorrectCar()
        {

            var mockRepository = new Mock<ICarRepository>();
            var expectedCar = new Car { Id = 1, Model = "BMW 7", Type = CarType.Premium };
            mockRepository.Setup(r => r.GetById(1)).Returns(expectedCar);

            var carService = new CarService(mockRepository.Object);

            var result = carService.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(expectedCar.Id, result.Id);
            Assert.Equal(expectedCar.Model, result.Model);
            Assert.Equal(expectedCar.Type, result.Type);
        }

        [Fact]
        public async Task AddCar_ShouldAddCarToRepository()
        {

            var mockRepository = new Mock<ICarRepository>();
            var carService = new CarService(mockRepository.Object);

            var newCar = new Car { Id = 3, Model = "Nissan Juke", Type = CarType.Suv };


            await carService.AddCar(newCar);

            mockRepository.Verify(r => r.AddCar(newCar), Times.Once);
        }

        [Fact]
        public async Task UpdateCar_ShouldUpdateCarInRepository()
        {
            var mockRepository = new Mock<ICarRepository>();
            var carService = new CarService(mockRepository.Object);

            var carToUpdate = new Car { Id = 2, Model = "Kia Sorento", Type = CarType.Suv };

            await carService.UpdateCar(carToUpdate);


            mockRepository.Verify(r => r.UpdateCar(carToUpdate), Times.Once);
        }

    }
}
