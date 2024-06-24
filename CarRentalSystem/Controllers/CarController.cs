using CarRentalSystem.Models;
using CarRentalSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpPost("add")]
        public IActionResult AddCar([FromBody] Car car)
        {
            _carService.AddCar(car);
            return Ok(new { Message = "Cars added successfully" });
        }

        [HttpGet("inventory")]
        public IActionResult GetInventory()
        {
            var cars = _carService.GetAll();
            return Ok(cars);
        }
    }
}
