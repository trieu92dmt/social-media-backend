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


# Command
 - Create Migration: `dotnet ef migrations add Init --project src/IdentityService.Infrastructure --startup-project src/IdentityService.Api`
 - Update databse: `dotnet ef database update --project src/IdentityService.Infrastructure --startup-project src/IdentityService.Api` 
 - Run Service: `docker compose -f infrastructure/docker/docker-compose.yml up -d`
 - Rebuild and run service: `docker compose -f infrastructure\docker\docker-compose.yml up -d --build post-service`