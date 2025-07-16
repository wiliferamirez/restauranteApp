# restauranteApp

Integrated platform for restaurant table reservation and user authentication, built with microservices architecture.

## Table of Contents

- [Project Overview](#project-overview)
- [Architecture](#architecture)
- [Features](#features)
- [Project Structure](#project-structure)
- [Technologies Used](#technologies-used)
- [How to Run the Project](#how-to-run-the-project)
- [API Endpoints](#api-endpoints)
- [Environment Variables & Configuration](#environment-variables--configuration)
- [Database Setup](#database-setup)
- [Testing](#testing)
- [Authors](#authors)
- [License](#license)

---

## Project Overview

restauranteApp is a full-stack solution for managing restaurant reservations and user authentication. It consists of three main microservices (auth, backend, frontend) and infrastructure configuration for deployment and API management.

## Architecture

- **Microservices**: Each core function (auth, reservation, frontend) is a separate service.
- **API Gateway**: Kong is used for routing and securing APIs.
- **Docker Compose**: All services are containerized for easy deployment.
- **Database**: SQL Server for persistent storage.

## Features

- User registration, login, and authentication (JWT & cookies).
- Table reservation management (CRUD operations).
- Admin dashboard for managing users and reservations.
- Responsive Angular frontend for user interaction.
- API documentation via Swagger.
- Password hashing and security.
- RabbitMQ integration for event publishing (reservation creation).
- CORS configuration for frontend-backend communication.

## Project Structure

```
restauranteApp/
├── auth/        # Authentication and user management microservice (.NET Core)
├── backend/     # Table reservation management microservice (.NET Core)
├── frontend/    # Angular web application
├── infra/       # Infrastructure (Docker Compose, Kong API Gateway)
```

## Technologies Used

- .NET Core (C#)
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Angular
- Docker
- Kong API Gateway
- RabbitMQ
- Swagger / OpenAPI

## How to Run the Project

1. **Clone the repository**
   ```powershell
   git clone <repo-url>
   cd restauranteApp
   ```

2. **Configure environment variables and configuration files**
   - Set connection strings in `auth/appsettings.json` and `backend/RestBook.Reservas/appsettings.json`.
   - Adjust Kong and Docker settings in `infra/`.

3. **Database Setup**
   - Run migrations to create the database:
     ```powershell
     cd backend/RestBook.Reservas
     dotnet ef migrations add InitialCreate
     dotnet ef database update
     ```

4. **Start all services with Docker Compose**
   ```powershell
   cd infra
   docker-compose up
   ```

5. **Access the application**
   - Frontend: `http://localhost:4200`
   - Swagger API docs: `http://localhost:<backend-port>/swagger`
   - Kong Gateway: `http://localhost:<kong-port>`

## API Endpoints

### Auth Service

- `POST /api/v1/auth/register` – Register a new user
- `POST /api/v1/auth/login` – Login and receive authentication token
- `GET /api/v1/auth/user` – Get user profile (protected)

### Reservation Service

| Method | Route                   | Description                       |
|--------|------------------------ |-----------------------------------|
| GET    | `/api/reservas`         | Get all reservations              |
| GET    | `/api/reservas/{id}`    | Get reservation by ID             |
| POST   | `/api/reservas`         | Create a new reservation          |
| PUT    | `/api/reservas/{id}`    | Update an existing reservation    |
| DELETE | `/api/reservas/{id}`    | Delete a reservation              |

- Swagger UI available at `/swagger` for API exploration.

## Environment Variables & Configuration

- **auth/appsettings.json**: Connection string, JWT secret, allowed origins.
- **backend/appsettings.json**: Connection string, RabbitMQ config.
- **infra/docker-compose.yml**: Service ports, environment variables.
- **kong/kong.yml**: API routes and plugins.

## Database Setup

- SQL Server is required.
- Migrations are managed via Entity Framework Core.
- Tables: Users, Reservations, Tables.

## Testing

- **Backend**: Unit and integration tests via xUnit.
- **Frontend**: Unit tests via Karma/Jasmine, e2e tests via Angular CLI.
  ```bash
  ng test        # Run unit tests
  ng e2e         # Run end-to-end tests
  ```

## Authors

- William Ramirez
- Romel Gualoto
- Daniel Diaz

## License

This project is for educational purposes only.
