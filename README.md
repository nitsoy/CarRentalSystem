# Car Rental System

## Overview
The Car Rental System is a .NET Core application designed to manage car rentals, calculate rental prices, track loyalty points for customers, and handle administrative tasks related to car inventory and rentals. This README provides an overview of the project structure, setup instructions, API endpoints, and testing information.

### Features
- Maintain an inventory of cars categorized as Premium, SUV, and Small.
- Calculate rental prices based on car type and rental duration.
- Track loyalty points for customers based on the type of car rented.
- Handle car rental transactions, including renting and returning cars with pricing adjustments for late returns.

## Technologies Used
- .NET Core 6
- Entity Framework Core (In-Memory Database for testing)
- ASP.NET Core Web API
- Moq for Mocking in Unit Tests
- xUnit for Unit Testing

## Project Structure
The project is structured as follows:

CarRentalSystem/
├── CarRentalSystem/ # Main project folder
│ ├── Controllers/ # API Controllers
│ ├── Models/ # Data models including Car, Customer, Rental, etc.
│ ├── Repositories/ # Data access layer interfaces and implementations
│ ├── Services/ # Business logic services including RentalService, CustomerService, etc.
│ ├── DTOs/ # Data Transfer Objects for API requests and responses
│ └── appsettings.json # Configuration settings
├── CarRentalSystem.Tests/ # Unit tests project
│ ├── Services/ # Unit tests for services
│ └── Mocks/ # Mocks for repositories and other dependencies
└── README.md # Project documentation

## Setup Instructions
Follow these steps to set up and run the Car Rental System locally:

### Prerequisites
- .NET Core 3.1 SDK or higher
- Git

### Steps
1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourusername/CarRentalSystem.git
   cd CarRentalSystem

    Restore packages:

    bash

dotnet restore

- Run the application:

dotnet run --project CarRentalSystem/CarRentalSystem.csproj

API will be available at: https://localhost:7243 (Swagger UI available at https://localhost:7243/swagger/index.html)

Run tests:

    dotnet test CarRentalSystem.Tests/CarRentalSystem.Tests.csproj

API Endpoints

The Car Rental System exposes the following endpoints:
Cars

    GET /api/cars: Get all cars in inventory.
    GET /api/cars/{id}: Get details of a specific car by ID.

Rentals

    POST /api/rentals/rent: Rent a car with specified details.
        Request Body: RentCarRequest
    POST /api/rentals/return: Return a rented car with specified details.
        Request Body: ReturnCarRequest

Customers

    GET /api/customers/{id}/loyaltyPoints: Get loyalty points for a customer by ID.

Testing

Unit tests are provided to ensure the correctness of business logic and API functionality. These tests utilize xUnit and Moq for mocking dependencies.

To run unit tests:

bash

dotnet test CarRentalSystem.Tests/CarRentalSystem.Tests.csproj

Contributors

    Justin Yepes

License

This project is licensed under the GNU GENERAL PUBLIC LICENSE - see the LICENSE file for details.