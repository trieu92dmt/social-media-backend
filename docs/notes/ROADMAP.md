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

## Backlog

Use this section to add upcoming implementation days before promoting them into
the weekly plan.

| Priority | Item | Notes |
| --- | --- | --- |
| High | Identity service foundation | API project, persistence, authentication flow |
| High | Post service foundation | API project, PostgreSQL schema, basic CRUD |
| Medium | API gateway | Routing, service discovery assumptions, auth forwarding |
| Medium | Shared building blocks | Contracts, common logging, validation, messaging helpers |
