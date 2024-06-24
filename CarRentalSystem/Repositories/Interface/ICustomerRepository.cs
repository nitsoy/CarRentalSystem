using CarRentalSystem.Models;

namespace CarRentalSystem.Repositories.Interface
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Customer GetById(int id);
        Task AddCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task SaveChanges();
    }
}
