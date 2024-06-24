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