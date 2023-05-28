using Construct.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace MediatR;

public static class MediatorExtensions
{
    /// <summary>
    /// Provides event handling for any domain objects in which domain events are added. Will trigger a 
    /// published event into the Mediator, where a defined handler can pick it up and perform the related
    /// actions
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static async Task DispatchDomainEvents(this IMediator mediator, DbContext context)
    {
        var entities = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}
