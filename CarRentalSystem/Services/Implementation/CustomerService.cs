using CarRentalSystem.Models;
using CarRentalSystem.Repositories.Interface;
using CarRentalSystem.Services.Interface;

namespace CarRentalSystem.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public IEnumerable<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public Customer GetById(int id)
        {
            return _customerRepository.GetById(id);
        }

        public async Task AddCustomer(Customer customer)
        {
            await _customerRepository.AddCustomer(customer);
        }

        public async Task AddLoyaltyPoints(int customerId, CarType carType)
        {
            var customer = GetById(customerId);
            if (customer != null)
            {
                customer.LoyaltyPoints += carType switch
                {
                    CarType.Premium => 5,
                    CarType.Suv => 3,
                    CarType.Small => 1,
                    _ => 0
                };
                await _customerRepository.UpdateCustomer(customer);
            }
        }
    }
}
