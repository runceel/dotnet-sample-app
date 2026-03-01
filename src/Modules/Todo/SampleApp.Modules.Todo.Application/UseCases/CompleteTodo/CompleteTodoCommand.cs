using MediatR;

namespace SampleApp.Modules.Todo.Application.UseCases.CompleteTodo;

public sealed record CompleteTodoCommand(Guid Id) : IRequest;
