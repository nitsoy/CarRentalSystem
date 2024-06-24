using CarRentalSystem.DTOs;
using CarRentalSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpPost("rent")]
        public async Task<ActionResult> RentCar([FromBody] RentCarRequest request)
        {
            await _rentalService.RentCar(request);
            var price = _rentalService.CalculateRentalPrice(request);
            return Ok(new { Price = price });
        }
        [HttpPost("rent-multiple")]
        public ActionResult<decimal> RentMultipleCars(List<RentCarRequest> rentals)
        {
            try
            {
                decimal totalPrice = 0;
                foreach (var rental in rentals)
                {
                    _rentalService.RentCar(rental);
                    totalPrice += _rentalService.CalculateRentalPrice(rental);
                }

                return Ok(totalPrice);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al rentar coches: {ex.Message}");
            }
        }
        [HttpPost("return")]
        public async Task<ActionResult> ReturnCar([FromBody] ReturnCarRequest request)
        {
            try
            {
                var extraCharges = await _rentalService.ReturnCar(request);
                return Ok(new { Message = "Car returned successfully", ExtraCharges = extraCharges });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
