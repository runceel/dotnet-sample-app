using MediatR;

namespace SampleApp.Modules.Todo.Application.UseCases.DeleteTodo;

public sealed record DeleteTodoCommand(Guid Id) : IRequest;
