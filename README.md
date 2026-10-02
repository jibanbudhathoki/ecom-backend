# Ecom

A production-grade ASP.NET Core e-commerce backend solution built with Clean Architecture principles.

## Architecture

Modular Monolith based on Clean Architecture:
- **Ecom.Domain**: Core business entities and logic (no dependencies).
- **Ecom.Application**: Use cases, interfaces, and business rules (depends on Domain).
- **Ecom.Infrastructure**: Persistence (EF Core), Identity, External Services (depends on Application).
- **Ecom.Api**: JWT-secured REST APIs for the customer front-end.
- **Ecom.Admin**: ASP.NET Core MVC application with Cookie authentication for administration.

## Dependency Rules
- Domain has NO external dependencies.
- Application depends ONLY on Domain.
- Infrastructure depends on Application and Domain.
- API and Admin depend on Infrastructure and Application.
- Database access is restricted to Infrastructure. Controllers should never use `DbContext` directly.

## Configuration & Setup

1. **Docker Setup**
   Ensure Docker is running, then start the PostgreSQL container:
   ```bash
   docker-compose up -d
   ```

2. **Database Configuration**
   PostgreSQL connection string is located in `appsettings.Development.json` (or can be provided via environment variables/user secrets).
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=ecom;Username=postgres;Password=change_me"
   }
   ```

## Running the Application

### Ecom.Api (Customer Facing)
Run the API project:
```bash
cd src/Ecom.Api
dotnet run
```
- Swagger UI: `https://localhost:<port>/swagger`
- Health Check: `https://localhost:<port>/health`

### Ecom.Admin (Back Office)
Run the Admin MVC project:
```bash
cd src/Ecom.Admin
dotnet run
```
- Admin Dashboard: `https://localhost:<port>/admin`

## Running Tests
Run all tests from the root directory:
```bash
dotnet test
```

## Development Conventions
- Use Central Package Management (`Directory.Packages.props`).
- Treat warnings appropriately (Configured in `Directory.Build.props`).
- Nullable reference types are enabled.
- Avoid god classes and controllers; split logic per feature/module.
- Adhere to the strict dependency flow of Clean Architecture.
