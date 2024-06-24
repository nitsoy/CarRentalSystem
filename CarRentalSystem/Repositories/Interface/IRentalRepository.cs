using CarRentalSystem.Models;

namespace CarRentalSystem.Repositories.Interface
{
    public interface IRentalRepository
    {
        IEnumerable<Rental> GetAll();
        Rental GetById(int id);
        Task AddRental(Rental rental);
        Task UpdateRental(Rental rental);
        Task SaveChanges();
    }
}
