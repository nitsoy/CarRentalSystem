using CarRentalSystem.Models;
using CarRentalSystem.Repositories.Interface;
using CarRentalSystem.Services.Interface;

namespace CarRentalSystem.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public IEnumerable<Car> GetAll() => _carRepository.GetAll();

        public Car GetById(int id) => _carRepository.GetById(id);

        public async Task AddCar(Car car) => await _carRepository.AddCar(car);

        public async Task UpdateCar(Car car) => await _carRepository.UpdateCar(car);
    }
}
