using CarRentalSystem.Repositories.Implementations;
using CarRentalSystem.Repositories.Interface;
using CarRentalSystem.Services.Implementation;
using CarRentalSystem.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CarRentalSystem
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddDbContext<Data.AppDbContext>(options =>
                options.UseInMemoryDatabase("CarRentalDb"));

            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarService, CarService>();

            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IRentalService, RentalService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }
    }
}
