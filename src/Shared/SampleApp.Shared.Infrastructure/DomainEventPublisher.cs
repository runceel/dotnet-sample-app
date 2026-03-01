using MediatR;
using SampleApp.Shared.Domain.SeedWork;

namespace SampleApp.Shared.Infrastructure;

public class DomainEventPublisher(IPublisher publisher)
{
    public async Task PublishEventsAsync(IEnumerable<Entity> entities, CancellationToken cancellationToken = default)
    {
        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        foreach (var entity in entities)
        {
            entity.ClearDomainEvents();
        }

        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent, cancellationToken);
        }
    }
}
