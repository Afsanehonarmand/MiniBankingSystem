# Mini Banking System

## Project Overview

This is a prototype backend system for a minimal banking platform built using .NET. It allows basic banking operations including:

- Creating accounts with an initial balance
- Depositing and withdrawing money
- Transferring funds between accounts
- Preventing overdrafts (negative balances)
- Retrieving transaction history (account statements)

The system uses purely in-memory storage — it does not use a database or file-based persistence. All data exists only during the lifetime of the application. The solution is structured using clean architecture principles to support future enhancements such as loan management, interest calculation, or multi-currency handling.

---

## Setup Instructions

### Prerequisites

- .NET SDK 9.0 or higher
- A terminal or an IDE that supports .NET (e.g., Visual Studio, VS Code)

### How to Run the Application

1. Clone the repository:

   ```bash
   git clone https://github.com/Afsanehonarmand/MiniBankingSystem.git
   cd MiniBankingSystem


2. Navigate to the console application folder: cd MiniBankingSystem.ConsoleApp

3.Run the demo app



Design Decisions
Architecture
The project is structured using clean architecture principles, separated into the following layers:

Domain: Core business entities and interfaces

Application: Business logic and service abstractions

Infrastructure: In-memory data storage

ConsoleApp: A console-based demo application

Tests: Unit tests using xUnit

In-Memory Repository
All account and transaction data is stored in memory using a dictionary structure. There is no persistent storage; all data is lost when the application stops.

Interfaces & Dependency Injection
The system is built around interfaces to promote loose coupling and high testability. Dependency Injection is used to inject repository and service implementations.

Async Programming
All service methods are asynchronous to reflect modern design patterns and simulate real-world I/O operations.

Testability
The architecture enables easy testing. Business logic and data layers are isolated and interact only through interfaces, making them simple to mock and validate in unit tests.

Assumptions
Each account is identified by a unique GUID.

All transactions are synchronous and atomic.

Negative balances are not allowed (overdrafts are rejected).

Currency values are represented as integers (e.g., cents).

The application is a standalone console app with no external dependencies.

How to Test
Unit Tests

The project includes unit tests written using xUnit. To run the test :

cd MiniBankingSystem.Tests
dotnet test

Covered Scenarios
Creating accounts

Depositing and withdrawing funds

Preventing overdrafts

Transferring funds between accounts

Viewing transaction history

Project Structure:
MiniBankingSystem/
│
├── Domain/               # Business entities and interfaces
├── Application/          # Services and abstractions
├── Infrastructure/       # In-memory repository (optionally with file storage)
├── ConsoleApp/           # Console-based demo runner
├── Tests/                # xUnit-based unit tests
└── README.md             # Project documentation


Final Notes
This project demonstrates the foundations of a banking system backend. It is designed for extensibility and easy refactoring. It can be further evolved into a production-ready system with API endpoints, real databases, authentication, and other enterprise features.

yaml
Copy
Edit


