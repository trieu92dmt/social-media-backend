# 45-Day Plan to Build a Social Media Backend Microservice

## Week 1 — Foundation + Infrastructure

## Day 1 — Setup Project Root

### Goals
Create a standard root structure.

1. Create repository `social-media-backend`
2. Install:
 - Docker Desktop
 - .NET 10 SDK
 - DBeaver
 - Postman
3. Create structure:
    social-media-backend
    │
    ├── gateway
    ├── services
    ├── building-blocks
    ├── infrastructure
    ├── docs

### Output
Push the first repository to GitHub.

## Day 2 — PostgreSQL + RabbitMQ

### Goals
Set up the first infrastructure.

1. Create docker compose file:
    ../infrastructure
    │
    ├── docker
    │   ├── .env
    │   ├── docker-compose.yml
2. Add service in docker compose:
 - PostgreSQL
 - RabbitMQ

### Output
 - Able to connect to the database
 - Able to access the RabbitMQ dashboard

## Day 3 — Redis + Seq

### Goals
Run redis and seq

1. Add service in docker compose:
 - Redis
 - Seq
2. Config log:
```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();
