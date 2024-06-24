using CarRentalSystem.Models;

namespace CarRentalSystem.Repositories.Interface
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAll();
        Car GetById(int id);
        Task AddCar(Car car);
        Task UpdateCar(Car car);
        Task SaveChanges();
    }
}
