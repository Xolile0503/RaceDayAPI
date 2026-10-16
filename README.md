# RaceDayAPI

## Project Overview

RaceDayAPI is an ASP.NET Core Web API developed in C# for managing race events and participant registrations. The project uses Microsoft SQL Server for data storage and Entity Framework Core for database operations.

## Technologies Used

- C#
- ASP.NET Core Web API
- .NET 8
- Entity Framework Core 8
- Microsoft SQL Server
- Visual Studio
- Git and GitHub

## Main Features

- User registration and login
- Participant and organiser roles
- Race event management
- Participant event registrations
- SQL Server database integration
- REST API endpoints

## Database

The application is designed to use a SQL Server database named `RaceDayDB`.

Configure the database connection string for your local environment before running the application. Do not commit passwords or private authentication keys to GitHub.

## How to Run the Project

1. Clone or download the repository.
2. Open the solution in Visual Studio.
3. Restore the NuGet packages.
4. Configure the SQL Server connection string.
5. Apply Entity Framework Core migrations if required.
6. Build the solution.
7. Run the API using Visual Studio.
8. Use the Swagger page to test the available endpoints, if Swagger is enabled.

## Development Status

The project is under development. Authentication security, role-based authorisation, database integration, testing, and other assignment requirements should be verified as development continues.
