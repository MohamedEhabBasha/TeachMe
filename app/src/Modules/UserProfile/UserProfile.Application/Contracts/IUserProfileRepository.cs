using UserProfile.Application.UserProfiles.Queries.GetFollowingInstructorsById;

namespace UserProfile.Application.Contracts;

public interface IUserProfileRepository
{
    Task AddAsync(Guid id);
    Task<Domain.UserProfiles.UserProfile?> GetUserProfileWithCategoryAsync(UserProfileId profileId, CancellationToken cancellationToken);
    Task<Domain.UserProfiles.UserProfile?> GetUserProfileAsync(Guid id, CancellationToken cancellationToken);
    Task<Domain.UserProfiles.UserProfile?> GetUserProfileWithFollowersAsync(Guid id, CancellationToken cancellationToken);
    Task<PaginatedResult<UserProfileDto>> GetFollowingInstructorsByIdAsync
        (StudentPaginationRequest studentRequest, CancellationToken cancellationToken);
    Task<int> GetFollowersCountByIdAsync(Guid id, CancellationToken cancellationToken);
}
