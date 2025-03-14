using System.Linq.Expressions;

namespace UserProfile.Application.UserProfiles.Queries.GetUserProfiles;

public class GetUserProfilesHandler(IUserProfileUnitOfWork unitOfWork)
    : IQueryHandler<GetUserProfilesQuery, GetUserProfilesResult>
{
    public async Task<GetUserProfilesResult> Handle(GetUserProfilesQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<Userprofile, bool>> criteria =
            u => u.Role == "Instructor" && 
                (string.IsNullOrEmpty(query.Category) || u.Categories.Any(c => c.Name == query.Category)) &&
                (string.IsNullOrEmpty(query.Search) || string.IsNullOrEmpty(u.Introduction) || u.Introduction.Contains(query.Search));

        var spec = new UserProfilesSpecification<UserProfileDto>
        (
            new UserProfileSpecParams
            (
                ApplyPagination: true,
                ApplyProjection: true, 
                Includes: ["category", "userFollows"],
                Criteria: criteria,
                OrderByDescending: query.MostFollowed? u => u.UserFollowers.Count : null
            )
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
            }
        );

        var userProfiles = await unitOfWork.UserProfileRepository.ListAsync(spec, cancellationToken);

        var count = await unitOfWork.UserProfileRepository.CountAsync(new CountSpecification(criteria));

        return new GetUserProfilesResult(new PaginatedResult<UserProfileDto>(userProfiles, count, query.PageNumber, query.PageSize));
    }
}
