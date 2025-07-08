# RestBook.Reservas – API para Gestión de Reservas
Desarrollado por: Romel Gualoto

Este módulo corresponde al backend del sistema **RestBook**, diseñado para gestionar reservas de mesas en restaurantes. Está construido con **ASP.NET Core Web API**, siguiendo una arquitectura limpia y principios **SOLID**. La solución incluye:

- Programación Orientada a Objetos (POO)
- Patrón Repositorio y Servicio
- Protección de contraseñas con `PasswordHasher`
- Integración con base de datos SQL Server
- Documentación de API con Swagger

---

## Tecnologías utilizadas

- ASP.NET Core 8 (Web API)
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- CORS
- Microsoft.AspNetCore.Identity
- Inyección de dependencias

---


Cada capa está separada para garantizar mantenibilidad, legibilidad y facilidad de pruebas.

---

##  Endpoints disponibles

| Método   | Ruta                   | Descripción                         |
|----------|------------------------|-------------------------------------|
| GET      | `/api/reservas`        | Obtener todas las reservas          |
| GET      | `/api/reservas/{id}`   | Obtener una reserva por ID          |
| POST     | `/api/reservas`        | Crear una nueva reserva             |
| PUT      | `/api/reservas/{id}`   | Actualizar una reserva existente    |
| DELETE   | `/api/reservas/{id}`   | Eliminar una reserva existente      |
- Se incluye un servicio de hash para proteger contraseñas o datos sensibles: `PasswordHasherService`

Puedes acceder a la interfaz Swagger en:  
 `http://localhost:5198/swagger`


##  Cómo ejecutar el proyecto

1. Navega a la carpeta del backend:
   ```bash
   cd backend/RestBook.Reservas
   dotnet run
2. Configura la cadena de conexión a SQL Server en appsettings.json
3.Para crear la base desde codigo:
    dotnet ef migrations add InitialCreate
    dotnet ef database update

