using Application.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using UserProfile.Application.Dtos;
using UserProfile.Application.UserProfiles.Queries.GetFollowingInstructorsById;

namespace UserProfile.Infrastructure.Data;

public class UserProfileRepository(UserProfileContext context, IMapper mapper) : IUserProfileRepository
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
    public async Task<PaginatedResult<UserProfileDto>> GetFollowingInstructorsByIdAsync
        (StudentPaginationRequest studentRequest, CancellationToken cancellationToken)
    {
        var pageNumber = studentRequest.PageNumber;
        var pageSize = studentRequest.PageSize;

        var userProfileId = new UserProfileId(studentRequest.StudentId);

        var query = context.UserProfiles.AsQueryable();

        query = query.Include(u => u.Categories)
                     .Include(u => u.UserFollowers)
                     .AsNoTracking()
                     .Where(u => u.UserFollowers.Any(f => f.StudentId == userProfileId));

        return await PaginatedResult<UserProfileDto>
            .CreateAsync(query.ProjectTo<UserProfileDto>(mapper.ConfigurationProvider), pageNumber, pageSize, cancellationToken);
    }
}
