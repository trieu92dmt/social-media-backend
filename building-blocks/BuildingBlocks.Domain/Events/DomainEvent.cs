namespace BuildingBlocks.Domain.Events;

public abstract class DomainEvent
{
    public Guid Id { get; init; }
        = Guid.NewGuid();

    public DateTime OccurredOn
        { get; init; }
        = DateTime.UtcNow;
}