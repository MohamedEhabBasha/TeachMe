namespace UserProfile.Infrastructure.Data;

public class UserProfileUnitOfWork(UserProfileContext context, IUserProfileRepository userProfileRepository) : IUserProfileUnitOfWork
{
    public IUserProfileRepository UserProfileRepository => userProfileRepository;

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}
