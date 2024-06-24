using CarRentalSystem.DTOs;
using CarRentalSystem.Models;

namespace CarRentalSystem.Services.Interface
{
    public interface IRentalService
    {
        decimal CalculateRentalPrice(RentCarRequest rental);
        decimal CalculateRentalExtraCharges(int carId, int extraDays);
        Task RentCar(RentCarRequest rental);
        Task<decimal> ReturnCar(ReturnCarRequest returnCarRequest);
        Rental GetRentalById(int id);
    }
}
