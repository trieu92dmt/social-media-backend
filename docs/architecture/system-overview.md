# System Overview

Services:
- Identity Service
- User Service
- Post Service
- Notification Service
- Search Service

Infrastructure:
- PostgreSQL
- RabbitMQ
- Redis
- Elasticsearch

# Dependency
| Package | Version | Purpose |
|---|---|---|
| `Microsoft.AspNetCore.OpenApi` | `10.0.12` | Generate OpenAPI documentation. |
| `Swashbuckle.AspNetCore` | `10.1.7` | Generate OpenAPI specifications and provide Swagger UI. |

# Command
 - Create Web API: `dotnet new webapi -n UserService.Api`
 - Create Class Library: `dotnet new classlib -n UserService.Application`
 - Add to Solution: `dotnet sln ../../../social-media-backend.sln add UserService.Api/UserService.Api.csproj`
 - Create Migration: `dotnet ef migrations add Init --project src/IdentityService.Infrastructure --startup-project src/IdentityService.Api`
 - Update databse: `dotnet ef database update --project src/IdentityService.Infrastructure --startup-project src/IdentityService.Api` 
 - Add Reference: `dotnet add reference ../IdentityService.Application/IdentityService.Application.csproj`
 - Run Service: `docker compose -f infrastructure/docker/docker-compose.yml up -d`
 - Rebuild and run service: `docker compose -f infrastructure\docker\docker-compose.yml up -d --build post-service`