using CarRentalSystem.Models;
using CarRentalSystem.Repositories.Interface;
using CarRentalSystem.Services.Implementation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public void GetAllCustomers_ShouldReturnAllCustomers()
        {

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "John Doe" },
            new Customer { Id = 2, Name = "Jane Smith" }
        };
            mockCustomerRepository.Setup(r => r.GetAll()).Returns(customers);

            var customerService = new CustomerService(mockCustomerRepository.Object);

            var result = customerService.GetAll();

            Assert.NotNull(result);
            Assert.Equal(customers.Count, result.Count());
            Assert.Equal(customers, result.ToList());
        }

        [Fact]
        public void GetCustomerById_ShouldReturnCorrectCustomer()
        {

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var expectedCustomer = new Customer { Id = 1, Name = "John Doe" };
            mockCustomerRepository.Setup(r => r.GetById(1)).Returns(expectedCustomer);

            var customerService = new CustomerService(mockCustomerRepository.Object);


            var result = customerService.GetById(1);


            Assert.NotNull(result);
            Assert.Equal(expectedCustomer.Id, result.Id);
            Assert.Equal(expectedCustomer.Name, result.Name);
        }

        [Fact]
        public async Task AddCustomer_ShouldAddCustomerToRepository()
        {

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var customerService = new CustomerService(mockCustomerRepository.Object);

            var newCustomer = new Customer { Id = 3, Name = "Alice Johnson" };


            await customerService.AddCustomer(newCustomer);


            mockCustomerRepository.Verify(r => r.AddCustomer(newCustomer), Times.Once);
        }
    }
}
