# ToDoApp - Backend

API REST para gestión de tareas construida con **Clean Architecture**, **.NET 10** y **SQL Server**.

## Tecnologías

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server (Docker)
- Swagger / OpenAPI

## Arquitectura

El proyecto sigue los principios de **Clean Architecture**, separado en 4 capas:

```
ToDoAPI/
├── ToDoAPI.Domain          # Entidades y contratos (sin dependencias externas)
├── ToDoAPI.Application     # Lógica de negocio e interfaces
├── ToDoAPI.Infrastructure  # Acceso a datos con EF Core y SQL Server
└── ToDoAPI.API             # Controllers y configuración HTTP
```

## Requisitos previos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Visual Studio 2026](https://visualstudio.microsoft.com/)

## Cómo correr el proyecto

### 1. Levantar SQL Server con Docker

```bash
docker compose up -d
```

### 2. Aplicar las migrations

```bash
dotnet ef database update --project ToDoAPI.Infrastructure --startup-project ToDoAPI.API
```

### 3. Correr la API

```bash
dotnet run --project ToDoAPI.API
```

La API estará disponible en `https://localhost:7000` y Swagger en `https://localhost:7000/swagger`.

## Endpoints

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | /api/todos | Obtener todas las tareas |
| GET | /api/todos/{id} | Obtener una tarea por ID |
| POST | /api/todos | Crear una nueva tarea |
| PUT | /api/todos/{id} | Actualizar una tarea |
| DELETE | /api/todos/{id} | Eliminar una tarea |
