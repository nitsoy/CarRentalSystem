using CarRentalSystem.Models;

namespace CarRentalSystem.Services.Interface
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        Customer GetById(int id);
        Task AddCustomer(Customer customer);
        Task AddLoyaltyPoints(int customerId, CarType carType);
    }
}
