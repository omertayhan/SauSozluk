# SauSozluk

SauSozluk is a dictionary application built using modern technologies including .NET 8, MSSQL, Dapper, and RabbitMQ. This project consists of multiple modules including APIs, web clients, common utilities, and projections.

## Technologies Used

- **.NET 8**: The core framework for the application.
- **MSSQL**: Database used in the API, utilizing code-first migrations.
- **Dapper**: Used in the projections for efficient data access.
- **RabbitMQ**: Used for messaging and communication between services.

## Project Structure

The project is organized into the following main components:

### 1. API

- **Core**: Core functionalities and domain logic.
- **Infrastructure**: Infrastructure-related code including persistence.

#### Infrastructure

- **BlazorSozluk.Infrastructure.Persistence**
  - **Context**
    - `BlazorSozlukContext.cs`: The database context for MSSQL using Entity Framework Core.
    - `SeedData.cs`: Seeds initial data into the database.
  - **EntityConfigurations**: Entity configurations for the database.
  - **Extensions**: Extension methods.
  - **Migrations**: Database migrations.
  - **Repositories**: Repositories for data access.

- **BlazorSozluk.Api.WebApi**
  - Contains the web API controllers and related services.

### 2. Clients

- **BlazorWeb**
  - **BlazorSozluk.WebApp**: The Blazor web application project.

### 3. Common

- **BlazorSozluk.Common**: Common utilities and shared code.

### 4. Projections

- **BlazorSozluk.Projections.FavoriteService**: Projection service for favorite items.
- **BlazorSozluk.Projections.UserService**: Projection service for user-related operations.
- **BlazorSozluk.Projections.VoteService**: Projection service for votes.

## Getting Started

### Prerequisites

- .NET 8 SDK
- MSSQL Server
- RabbitMQ

### Setting Up the Project

1. Clone the repository:

    ```bash
    git clone https://github.com/omertayhan/SauSozluk.git
    ```

2. Navigate to the project directory:

    ```bash
    cd sausozluk
    ```

3. Restore the .NET dependencies:

    ```bash
    dotnet restore
    ```

4. Apply the migrations to the MSSQL database:

    ```bash
    dotnet ef database update --project src/Api/Infrastructure/BlazorSozluk.Infrastructure.Persistence
    ```

5. Run the API project:

    ```bash
    dotnet run --project src/Api/WebApi/BlazorSozluk.Api.WebApi
    ```

6. Run the Blazor web application:

    ```bash
    dotnet run --project src/Clients/BlazorWeb/BlazorSozluk.WebApp
    ```

### Configuration

Ensure that the connection strings and RabbitMQ settings are correctly configured in the `appsettings.json` files for the respective projects.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any improvements or bug fixes.

