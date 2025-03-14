using Infrastructure;

namespace UserProfile.Infrastructure.Data;

public class UserProfileUnitOfWork(UserProfileContext context, IUserProfileRepository userProfileRepository)
    : UnitOfWork(context), IUserProfileUnitOfWork
{
    public IUserProfileRepository UserProfileRepository => userProfileRepository;
}
