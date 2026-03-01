using MediatR;
using SampleApp.Modules.Todo.Application.DTOs;

namespace SampleApp.Modules.Todo.Application.UseCases.CreateTodo;

public sealed record CreateTodoCommand(string Title, string? Description) : IRequest<TodoItemDto>;
