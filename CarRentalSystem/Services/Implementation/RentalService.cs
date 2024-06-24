using CarRentalSystem.DTOs;
using CarRentalSystem.Models;
using CarRentalSystem.Repositories.Interface;
using CarRentalSystem.Services.Interface;

namespace CarRentalSystem.Services.Implementation
{
    public class RentalService : IRentalService
    {
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRentalRepository _rentalRepository;

        private readonly decimal PremiumPrice = 300m;
        private readonly decimal smallPrice = 50m;
        private readonly decimal SuvPrice = 150m;

        public RentalService(ICarRepository carRepository, ICustomerRepository customerRepository,
            IRentalRepository rentalRepository)
        {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _rentalRepository = rentalRepository;
        }
        public Rental GetRentalById(int id) => _rentalRepository.GetById(id);
        public decimal CalculateRentalPrice(RentCarRequest rentCarRequest)
        {
            var car = _carRepository.GetById(rentCarRequest.CarId) ?? throw new Exception("Car not found");

            var basePrice = car.Type switch
            {
                CarType.Premium => PremiumPrice,
                CarType.Suv => SuvPrice,
                CarType.Small => smallPrice,
                _ => throw new Exception("Unknown car type")
            };

            var totalPrice = car.Type switch
            {
                CarType.Premium => basePrice * rentCarRequest.DurationDays,
                CarType.Suv => CalculateSUVPrice(basePrice, rentCarRequest.DurationDays),
                CarType.Small => CalculateSmallPrice(basePrice, rentCarRequest.DurationDays),
                _ => throw new Exception("Unknown car type")
            };

            return totalPrice;
        }

        public decimal CalculateRentalExtraCharges(int carId, int extraDays)
        {
            var car = _carRepository.GetById(carId) ?? throw new Exception("Car not found");
            var extraPrice = car.Type switch
            {
                CarType.Premium => PremiumPrice + (PremiumPrice * 0.2m),
                CarType.Suv => SuvPrice * 1.6m,
                CarType.Small => smallPrice * 1.3m,
                _ => throw new Exception("Unknown car type")
            };

            return extraPrice * extraDays;
        }
        private decimal CalculateSUVPrice(decimal basePrice, int days)
        {
            decimal totalPrice = 0;
            if (days <= 7)
            {
                totalPrice = basePrice * days;
            }
            else if (days > 7 && days <= 30)
            {
                totalPrice = (basePrice * 7) + (0.8m * basePrice * (days - 7));
            }
            else if (days > 30)
            {
                totalPrice = (basePrice * 7) + (0.8m * basePrice * 23) + (0.5m * basePrice * (days - 30));
            }
            return totalPrice;
        }

        private decimal CalculateSmallPrice(decimal basePrice, int days)
        {
            decimal totalPrice;
            if (days <= 7)
            {
                totalPrice = basePrice * days;
            }
            else
            {
                totalPrice = (basePrice * 7) + (0.6m * basePrice * (days - 7));
            }
            return totalPrice;
        }
        private void AddLoyaltyPoints(Customer customer, CarType carType)
        {
            customer.LoyaltyPoints += carType switch
            {
                CarType.Premium => 5,
                CarType.Suv => 3,
                CarType.Small => 1,
                _ => 0
            };
            _customerRepository.UpdateCustomer(customer);
        }

        public async Task RentCar(RentCarRequest rentCarRequest)
        {
            var car = _carRepository.GetById(rentCarRequest.CarId);
            if (car == null || car.IsRented)
            {
                throw new Exception("Car is not available");
            }

            car.IsRented = true;
            await _carRepository.UpdateCar(car);

            var rental = new Rental
            {
                CarId = rentCarRequest.CarId,
                StartDate = rentCarRequest.StartDate,
                DurationDays = rentCarRequest.DurationDays,
                CustomerId = rentCarRequest.CustomerId
            };

            await _rentalRepository.AddRental(rental);

            var customer = _customerRepository.GetById(rentCarRequest.CustomerId);
            if (customer != null)
            {
                AddLoyaltyPoints(customer, car.Type);
            }
        }

        public async Task<decimal> ReturnCar(ReturnCarRequest returnCarRequest)
        {
            var rental = _rentalRepository.GetById(returnCarRequest.RentalId) ?? throw new Exception("Rental not found");

            var car = _carRepository.GetById(rental.CarId) ?? throw new Exception("Car not found");

            car.IsRented = false;
            await _carRepository.UpdateCar(car);

            rental.ReturnDate = returnCarRequest.ReturnDate;
            await _rentalRepository.UpdateRental(rental);

            var extraDays = (returnCarRequest.ReturnDate - (rental.StartDate.AddDays(rental.DurationDays))).Days;
            var extraCharges = extraDays > 0 ? CalculateRentalExtraCharges(rental.Car.Id, extraDays) : 0;

            return extraCharges;
        }
    }
}
