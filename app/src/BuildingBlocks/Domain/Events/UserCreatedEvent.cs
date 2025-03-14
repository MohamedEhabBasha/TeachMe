namespace Domain.Events;

public record UserCreatedEvent(Guid UserId, string Name, string Role) : IDomainEvent;
