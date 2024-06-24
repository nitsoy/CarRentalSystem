using CarRentalSystem.Data;
using CarRentalSystem.Models;
using CarRentalSystem.Repositories.Interface;

namespace CarRentalSystem.Repositories.Implementations
{
    public class RentalRepository : IRentalRepository
    {
        private readonly AppDbContext _context;

        public RentalRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Rental> GetAll()
        {
            return _context.Rentals.ToList();
        }

        public Rental GetById(int id)
        {
            return _context.Rentals.Find(id);
        }

        public async Task AddRental(Rental rental)
        {
            _context.Rentals.Add(rental);
            await SaveChanges();
        }

        public async Task UpdateRental(Rental rental)
        {
            _context.Rentals.Update(rental);
            await SaveChanges();
        }

        public Task SaveChanges() => _context.SaveChangesAsync();
    }
}
