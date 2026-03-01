using SampleApp.Shared.Domain.SeedWork;

namespace SampleApp.Modules.Todo.Domain.Events;

public sealed record TodoItemCompletedEvent(Guid TodoId) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}
