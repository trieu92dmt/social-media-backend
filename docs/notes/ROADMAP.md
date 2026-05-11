# 45-Day Social Media Backend Roadmap

## Overview

This roadmap tracks the implementation plan for a social media backend built with
microservices, .NET, PostgreSQL, RabbitMQ, Redis, Docker, and supporting
observability tools.

### Objectives

- Establish a clean repository and documentation structure.
- Build the core infrastructure needed for local development.
- Add service foundations incrementally and keep each milestone verifiable.
- Maintain a practical output checklist for every implementation day.

### Progress Legend

| Status | Meaning |
| --- | --- |
| `Todo` | Not started |
| `In Progress` | Currently being implemented |
| `Done` | Completed and verified |

## Milestones

| Phase | Scope | Target |
| --- | --- | --- |
| Foundation | Repository layout, tooling, Docker infrastructure | Week 1 |
| Core Services | Identity, posts, media, and relationships | Weeks 2-4 |
| Integration | Messaging, caching, gateway, and cross-service workflows | Weeks 5-6 |
| Hardening | Observability, testing, deployment readiness | Final days |

## Week 1: Foundation and Infrastructure

### Day 1: Project Root Setup

**Status:** `Done`

**Goal:** Create a standard repository structure for the backend solution.

**Tasks**

- [x] Create the `social-media-backend` repository.
- [x] Install required development tools:
  - Docker Desktop
  - .NET 10 SDK
  - DBeaver
  - Postman
- [x] Create the initial project structure:

```text
social-media-backend/
|-- gateway/
|-- services/
|-- building-blocks/
|-- infrastructure/
`-- docs/
```

**Output**

- Initial repository is pushed to GitHub.

### Day 2: PostgreSQL and RabbitMQ

**Status:** `Done`

**Goal:** Add the first local infrastructure services.

**Tasks**

- [x] Create the Docker infrastructure folder:

```text
infrastructure/
`-- docker/
    |-- .env
    `-- docker-compose.yml
```

- [x] Add PostgreSQL to `docker-compose.yml`.
- [x] Add RabbitMQ to `docker-compose.yml`.

**Output**

- PostgreSQL is running and reachable from a database client.
- RabbitMQ management dashboard is accessible.

### Day 3: Redis and Seq

**Status:** `Done`

**Goal:** Add caching and centralized local logging infrastructure.

**Tasks**

- [x] Add Redis to `docker-compose.yml`.
- [x] Add Seq to `docker-compose.yml`.
- [x] Configure Serilog to write logs to console and Seq:

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();
```

**Output**

- Redis is running and available for local services.
- Seq dashboard is accessible.
- Application logs can be viewed in Seq.

### Day 4: Post Publishing and Search Indexing

**Status:** `Done`

**Goal:** Create the first end-to-end flow from post creation to Elasticsearch search.

**Tasks**

- [x] Add PostDb, Elasticsearch, and Kibana to `docker-compose.yml`.
- [x] Create the `Post` entity in `PostService.Domain`.
- [x] Add `PostDbContext` and repository implementation in `PostService.Infrastructure`.
- [x] Add post application abstractions and command handler in `PostService.Application`.
- [x] Add `PostController` in `PostService.Api`.
- [x] Add EF Core migration for post persistence.
- [x] Add shared `PostCreatedIntegrationEvent` contract in `building-blocks`.
- [x] Configure MassTransit publisher in `PostService`.
- [x] Configure Elasticsearch client in `SearchService.Infrastructure`.
- [x] Create search abstractions in `SearchService.Application`.
- [x] Implement Elasticsearch indexing and search in `SearchService.Infrastructure`.
- [x] Add `PostCreatedConsumer` to consume post events and index documents.
- [x] Add search API endpoint in `SearchService.Api`.

**Output**

- A post can be saved to PostDb.
- `PostService` publishes a `PostCreatedIntegrationEvent`.
- `SearchService` consumes the event and indexes the post in Elasticsearch.
- Indexed posts can be retrieved through the Search API.
- Kibana is available for inspecting Elasticsearch data.

### Day 5: API Gateway and Health Checks

**Status:** `Done`

**Goal:** Introduce a YARP-based API Gateway and basic service health checks.

**Tasks**

- [x] Create `ApiGateway` under the `gateway` folder.
- [x] Configure YARP reverse proxy routes and clusters.
- [x] Add PostDb health check configuration in `PostService.Api`.
- [x] Create Dockerfiles for `ApiGateway`, `PostService.Api`, and `SearchService.Api`.
- [x] Add `ApiGateway`, `PostService.Api`, and `SearchService.Api` to `docker-compose.yml`.
- [x] Verify gateway routing to backend services through Docker Compose.

**Output**

- API Gateway is available as the single entry point for service requests.
- Reverse proxy routes forward requests to the correct backend services.
- `PostService.Api` exposes a working health check endpoint.
- Gateway and services can run together from Docker Compose.

### Day 6: Shared Building Blocks

**Status:** `Done`

**Goal:** Establish shared building blocks for common domain, application, and infrastructure concerns.

**Tasks**

- [x] Create `BuildingBlocks.Application` under `building-blocks`.
- [x] Create `BuildingBlocks.Domain` under `building-blocks`.
- [x] Create `BuildingBlocks.Infrastructure` under `building-blocks`.
- [x] Add shared `Result` type in `BuildingBlocks.Application`.
- [x] Add shared `BaseEntity` type in `BuildingBlocks.Domain`.
- [x] Add shared `DomainEvent` type in `BuildingBlocks.Domain`.
- [x] Prepare the structure for reusable cross-service code.

**Output**

- Shared building block projects are available for services to reference.
- Common application result handling is centralized through `Result`.
- Domain entities can inherit from `BaseEntity`.
- Domain events can use the shared `DomainEvent` base type.

## Backlog

Use this section to add upcoming implementation days before promoting them into
the weekly plan.

| Priority | Item | Notes |
| --- | --- | --- |
| High | Identity service foundation | API project, persistence, authentication flow |
| High | Post service foundation | API project, PostgreSQL schema, basic CRUD |
| Medium | API gateway | Routing, service discovery assumptions, auth forwarding |
| Medium | Shared building blocks | Contracts, common logging, validation, messaging helpers |
