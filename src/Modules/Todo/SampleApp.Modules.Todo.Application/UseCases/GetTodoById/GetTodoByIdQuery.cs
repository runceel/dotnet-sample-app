using MediatR;
using SampleApp.Modules.Todo.Application.DTOs;

namespace SampleApp.Modules.Todo.Application.UseCases.GetTodoById;

public sealed record GetTodoByIdQuery(Guid Id) : IRequest<TodoItemDto?>;
