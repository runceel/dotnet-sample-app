using SampleApp.Shared.Domain.SeedWork;

namespace SampleApp.Modules.Todo.Domain.Events;

public sealed record TodoItemCreatedEvent(Guid TodoId, string Title) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}
