namespace UserProfile.Application.Contracts;

public interface IUserProfileRepository
{
    Task AddAsync(Guid id);
    Task<Domain.UserProfiles.UserProfile?> GetUserProfileWithCategoryAsync(UserProfileId profileId, CancellationToken cancellationToken);
    Task<Domain.UserProfiles.UserProfile?> GetUserProfileAsync(Guid id, CancellationToken cancellationToken);
    Task<Domain.UserProfiles.UserProfile?> GetUserProfileWithFollowersAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Domain.UserProfiles.UserProfile>> GetFollowingInstructorsByIdAsync(Guid studentId, CancellationToken cancellationToken);
}
