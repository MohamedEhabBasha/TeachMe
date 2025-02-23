using UserProfile.Application.Contracts;
using UserProfile.Application.Dtos;
using UserProfile.Domain.UserProfiles.ValueObjects;

namespace UserProfile.Infrastructure.Data;

public class UserProfileRepository(UserProfileContext context) : IUserProfileRepository
{
    public async Task AddAsync(Guid id)
    {
        var userProfile = Domain.UserProfiles.UserProfile.Create(id);

        await context.UserProfiles.AddAsync(userProfile);
    }

    public async Task<Domain.UserProfiles.UserProfile?> GetUserProfileWithCategoryAsync(UserProfileId profileId, CancellationToken cancellationToken)
    {
        return await context.UserProfiles
            .Include(u => u.Categories)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == profileId, cancellationToken: cancellationToken);
    }
    public async Task<Domain.UserProfiles.UserProfile?> GetUserProfileWithFollowersAsync(Guid id, CancellationToken cancellationToken)
    {
        var userProfileId = new UserProfileId(id);

        return await context.UserProfiles.Include(u => u.UserFollowers)
            .FirstOrDefaultAsync(u => u.Id == userProfileId, cancellationToken: cancellationToken);
    }
    public async Task<Domain.UserProfiles.UserProfile?> GetUserProfileAsync(Guid id, CancellationToken cancellationToken)
    {
        var userProfileId = new UserProfileId(id);

        return await context.UserProfiles
            .FirstOrDefaultAsync(u => u.Id == userProfileId, cancellationToken: cancellationToken);
    }
    public async Task<IReadOnlyCollection<Domain.UserProfiles.UserProfile>> GetFollowingInstructorsByIdAsync(Guid studentId, CancellationToken cancellationToken)
    {
        var userProfileId = new UserProfileId(studentId);

        var instructors = await context.UserProfiles
            .Include(u => u.Categories)
            .Include(u => u.UserFollowers)
            .AsNoTracking()
            .Where(u => u.UserFollowers.Any(f => f.StudentId == userProfileId))
            .ToListAsync(cancellationToken);

        return instructors;
    }
}
