using CarRentalSystem.Models;

namespace CarRentalSystem.Services.Interface
{
    public interface ICarService
    {
        IEnumerable<Car> GetAll();
        Car GetById(int id);
        Task AddCar(Car car);
        Task UpdateCar(Car car);

    }
}
