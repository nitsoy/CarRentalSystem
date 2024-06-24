using CarRentalSystem.Data;
using CarRentalSystem.Models;
using CarRentalSystem.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;

public class CarRepository : ICarRepository
{
    private readonly AppDbContext _context;

    public CarRepository(AppDbContext context)
    {
        _context = context;

        var cars = new List<Car> {
                new Car { Id = 1, Model = "BMW 7", Type = CarType.Premium },
                new Car { Id = 2, Model = "Kia Sorento", Type = CarType.Suv },
                new Car { Id = 3, Model = "Nissan Juke", Type = CarType.Suv },
                new Car { Id = 4, Model = "Seat Ibiza", Type = CarType.Small }
        };

        var existingCarIds = _context.Cars.Select(c => c.Id).ToHashSet();

        var carsToAdd = cars.Where(car => !existingCarIds.Contains(car.Id)).ToList();

        if (carsToAdd.Any())
        {
            _context.Cars.AddRange(carsToAdd);
            SaveChanges();
        }
    }

    public IEnumerable<Car> GetAll() => _context.Cars.ToList();

    public Car GetById(int id)
    {
        return _context.Cars.Find(id);
    }

    public async Task AddCar(Car car)
    {
        _context.Cars.Add(car);
        await SaveChanges();
    }

    public async Task UpdateCar(Car car)
    {
        _context.Cars.Update(car);
        await SaveChanges();
    }
    public Task SaveChanges() => _context.SaveChangesAsync();
}
