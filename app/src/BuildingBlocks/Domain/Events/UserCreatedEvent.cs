namespace Domain.Events;

public record UserCreatedEvent(Guid UserId) : IDomainEvent;
