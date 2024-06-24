using CarRentalSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}/loyalty-points")]
        public IActionResult GetLoyaltyPoints(int id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null)
            {
                return NotFound(new { Message = "Customer not found" });
            }
            return Ok(new { customer.LoyaltyPoints });
        }

        [HttpGet("all")]
        public IActionResult GetAllCustomers()
        {
            var customers = _customerService.GetAll();
            return Ok(customers);
        }
    }
}
