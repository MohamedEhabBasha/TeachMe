using UserProfile.Application.Contracts;

namespace UserProfile.Application.UserProfiles.EventHandlers.Domain;

public class UserCreatedEventHandler(IUserProfileUnitOfWork unitOfWork, ILogger<UserCreatedEventHandler> logger) : INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        await unitOfWork.UserProfileRepository.AddAsync(domainEvent.UserId);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
