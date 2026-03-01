# Microservices Migration Guide

## Overview

This modular monolith is designed to be easily decomposed into microservices when the need arises. Each module is already isolated with well-defined boundaries.

## Current Modular Monolith Structure

Each module follows the same pattern:
- Independent domain model
- Independent database context
- Public API via minimal API endpoints
- Internal implementation hidden behind interfaces

## Migration Strategy

### Step 1: Identify Module Boundaries

Each module in `src/Modules/` maps to a potential microservice:
- `Todo` module → Todo microservice

### Step 2: Extract Module to Separate Service

1. Create a new ASP.NET Core Web API project
2. Move the module's Domain, Application, Infrastructure, and Api projects
3. Keep the same endpoint contracts to avoid breaking clients

### Step 3: Update AppHost

Add the new service to the Aspire AppHost:

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var todoApi = builder.AddProject<Projects.SampleApp_Todo_Api>("todo-api");

builder.AddProject<Projects.SampleApp_Web_Server>("web-server")
    .WithReference(todoApi);

builder.Build().Run();
```

### Step 4: Update Client Communication

Replace direct in-process calls with HTTP calls using typed clients:

```csharp
// Register typed HTTP client with service discovery
builder.Services.AddHttpClient<ITodoApiClient, TodoApiClient>(
    client => client.BaseAddress = new Uri("http://todo-api"));
```

### Step 5: Handle Cross-Module Communication

Replace direct method calls between modules with:
- **HTTP APIs**: For synchronous request/response
- **Message queues**: For asynchronous event-driven communication (e.g., Azure Service Bus, RabbitMQ)

## Database Considerations

Each module already uses its own `DbContext`. For microservices:

1. Move each module's SQLite database to its own dedicated database
2. Update connection strings in the AppHost configuration
3. Use Aspire's database resources:

```csharp
var postgres = builder.AddPostgres("postgres");
var todoDb = postgres.AddDatabase("todo-db");

builder.AddProject<Projects.SampleApp_Todo_Api>("todo-api")
    .WithReference(todoDb);
```

## Strangler Fig Pattern

You can incrementally migrate by using the strangler fig pattern:

1. Keep the monolith running
2. Extract one module at a time to a microservice
3. Route traffic to the new service using a reverse proxy
4. Decommission the module from the monolith once stable

## When to Migrate

Consider migrating to microservices when:
- Individual modules need independent scaling
- Different modules require different technology stacks
- Team size requires independent deployment pipelines
- Module load or failure isolation becomes critical
