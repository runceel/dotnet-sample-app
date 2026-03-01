using MediatR;
using SampleApp.Modules.Todo.Application.DTOs;

namespace SampleApp.Modules.Todo.Application.UseCases.GetTodos;

public sealed record GetTodosQuery : IRequest<IReadOnlyList<TodoItemDto>>;
