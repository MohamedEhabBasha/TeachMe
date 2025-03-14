namespace UserProfile.Application.UserProfiles.EventHandlers.Domain;

public class UserCreatedEventHandler(IUserProfileUnitOfWork unitOfWork, ILogger<UserCreatedEventHandler> logger) : INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var userProfile = Userprofile.Create(domainEvent.UserId, domainEvent.Name, domainEvent.Role);

        await unitOfWork.UserProfileRepository.AddAsync(userProfile);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
