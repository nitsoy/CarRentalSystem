using CarRentalSystem.Data;
using CarRentalSystem.Models;
using CarRentalSystem.Repositories.Interface;

namespace CarRentalSystem.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
            var customers = new List<Customer> {
                new Customer { Id = 1, Name = "John Doe", LoyaltyPoints = 0 }
            };

            var existingcustomers = _context.Customers.Select(c => c.Id).ToHashSet();

            var customersToAdd = customers.Where(cus => !existingcustomers.Contains(cus.Id)).ToList();

            if (customersToAdd.Any())
            {
                context.Customers.AddRange(customersToAdd);
                context.SaveChanges();
            }
        }

        public IEnumerable<Customer> GetAll() => _context.Customers.ToList();

        public Customer GetById(int id) => _context.Customers.Find(id);

        public async Task AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await SaveChanges();
        }

        public async Task UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            await SaveChanges();
        }

        public Task SaveChanges() => _context.SaveChangesAsync();
    }
}
