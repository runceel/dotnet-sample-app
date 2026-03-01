using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SampleApp.Modules.Todo.Application.UseCases.CompleteTodo;
using SampleApp.Modules.Todo.Application.UseCases.CreateTodo;
using SampleApp.Modules.Todo.Application.UseCases.DeleteTodo;
using SampleApp.Modules.Todo.Application.UseCases.GetTodoById;
using SampleApp.Modules.Todo.Application.UseCases.GetTodos;

namespace SampleApp.Modules.Todo.Api;

public static class TodoEndpoints
{
    public static IEndpointRouteBuilder MapTodoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/todos")
            .WithTags("Todos")
            .WithOpenApi();

        group.MapGet("/", async (IMediator mediator, CancellationToken ct) =>
        {
            var todos = await mediator.Send(new GetTodosQuery(), ct);
            return Results.Ok(todos);
        })
        .WithName("GetTodos")
        .WithSummary("Get all todo items");

        group.MapGet("/{id:guid}", async (Guid id, IMediator mediator, CancellationToken ct) =>
        {
            var todo = await mediator.Send(new GetTodoByIdQuery(id), ct);
            return todo is null ? Results.NotFound() : Results.Ok(todo);
        })
        .WithName("GetTodoById")
        .WithSummary("Get a todo item by ID");

        group.MapPost("/", async (CreateTodoCommand command, IMediator mediator, CancellationToken ct) =>
        {
            var todo = await mediator.Send(command, ct);
            return Results.CreatedAtRoute("GetTodoById", new { id = todo.Id }, todo);
        })
        .WithName("CreateTodo")
        .WithSummary("Create a new todo item");

        group.MapPut("/{id:guid}/complete", async (Guid id, IMediator mediator, CancellationToken ct) =>
        {
            await mediator.Send(new CompleteTodoCommand(id), ct);
            return Results.NoContent();
        })
        .WithName("CompleteTodo")
        .WithSummary("Mark a todo item as complete");

        group.MapDelete("/{id:guid}", async (Guid id, IMediator mediator, CancellationToken ct) =>
        {
            await mediator.Send(new DeleteTodoCommand(id), ct);
            return Results.NoContent();
        })
        .WithName("DeleteTodo")
        .WithSummary("Delete a todo item");

        return app;
    }
}
