using MediatR;

namespace SampleApp.Shared.Domain.SeedWork;

public interface IDomainEvent : INotification
{
    DateTimeOffset OccurredOn { get; }
}
