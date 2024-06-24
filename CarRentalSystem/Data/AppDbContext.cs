using CarRentalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Customer> Customers { get; set; }
        protected readonly IConfiguration Configuration;
        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity
            options.UseInMemoryDatabase("CarRentalDb");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, Model = "BMW 7", Type = CarType.Premium },
                new Car { Id = 2, Model = "Kia Sorento", Type = CarType.Suv },
                new Car { Id = 3, Model = "Nissan Juke", Type = CarType.Suv },
                new Car { Id = 4, Model = "Seat Ibiza", Type = CarType.Small }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "John Doe", LoyaltyPoints = 0 }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
