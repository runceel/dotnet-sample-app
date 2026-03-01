# Clean Architecture Guide

## Overview

This application follows Clean Architecture principles organized as a modular monolith. Each module is self-contained and follows the same layered structure.

## Architecture Layers

```
┌─────────────────────────────────────────────────────────┐
│                    Presentation Layer                    │
│              (Web.Client, Web.Server, Api)               │
├─────────────────────────────────────────────────────────┤
│                   Application Layer                      │
│         (Use Cases, DTOs, Command/Query Handlers)        │
├─────────────────────────────────────────────────────────┤
│                     Domain Layer                         │
│        (Entities, Value Objects, Domain Events)          │
├─────────────────────────────────────────────────────────┤
│                 Infrastructure Layer                     │
│          (EF Core, Repositories, Persistence)            │
└─────────────────────────────────────────────────────────┘
```

## Project Structure

```
src/
├── Shared/
│   ├── SampleApp.Shared.Domain/          # Shared domain primitives
│   │   └── SeedWork/
│   │       ├── Entity.cs                 # Base entity with domain events
│   │       ├── AggregateRoot.cs          # Aggregate root base class
│   │       ├── IDomainEvent.cs           # Domain event interface
│   │       └── IUnitOfWork.cs            # Unit of work interface
│   └── SampleApp.Shared.Infrastructure/ # Shared infrastructure utilities
│
├── Modules/
│   └── Todo/
│       ├── SampleApp.Modules.Todo.Domain/         # Domain layer
│       ├── SampleApp.Modules.Todo.Application/    # Application layer
│       ├── SampleApp.Modules.Todo.Infrastructure/ # Infrastructure layer
│       └── SampleApp.Modules.Todo.Api/            # API endpoints
│
├── Web/
│   ├── SampleApp.Web.Client/  # Blazor WebAssembly client
│   └── SampleApp.Web.Server/  # ASP.NET Core host server
│
├── AppHost/
│   └── SampleApp.AppHost/     # .NET Aspire orchestration
│
└── ServiceDefaults/
    └── SampleApp.ServiceDefaults/  # Shared service configuration
```

## Domain Layer

The domain layer contains:

- **Entities**: Business objects with identity (e.g., `TodoItem`)
- **Value Objects**: Immutable objects defined by their value (e.g., `TodoTitle`)
- **Domain Events**: Events that capture domain state changes
- **Repository Interfaces**: Abstractions for data access

### Aggregate Root Pattern

Every aggregate inherits from `AggregateRoot` which extends `Entity`. Domain events are raised within the aggregate and stored until published.

```csharp
public class TodoItem : AggregateRoot
{
    public static TodoItem Create(TodoTitle title, string? description = null)
    {
        var todo = new TodoItem { ... };
        todo.AddDomainEvent(new TodoItemCreatedEvent(todo.Id, title.Value));
        return todo;
    }
}
```

## Application Layer

Uses the CQRS pattern via MediatR:

- **Commands**: Mutate state (e.g., `CreateTodoCommand`, `CompleteTodoCommand`)
- **Queries**: Read state (e.g., `GetTodosQuery`, `GetTodoByIdQuery`)
- **Handlers**: Execute the commands/queries
- **DTOs**: Data transfer objects for API responses

## Infrastructure Layer

- **EF Core DbContext** implements `IUnitOfWork`
- **Repositories** implement domain repository interfaces
- **EF Configurations** use `IEntityTypeConfiguration<T>` for clean mapping

## Dependency Flow

Dependencies always point inward:
- Api → Application + Infrastructure
- Application → Domain
- Infrastructure → Domain
- Domain → (nothing)
