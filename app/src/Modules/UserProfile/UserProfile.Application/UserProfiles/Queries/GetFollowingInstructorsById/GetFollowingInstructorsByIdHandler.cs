using System.Linq.Expressions;

namespace UserProfile.Application.UserProfiles.Queries.GetFollowingInstructorsById;

public class GetFollowingInstructorsByIdHandler(IUserProfileUnitOfWork unitOfWork)
    : IQueryHandler<GetFollowingInstructorsByIdQuery, GetFollowingInstructorsByIdResult>
{
    public async Task<GetFollowingInstructorsByIdResult> Handle(GetFollowingInstructorsByIdQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<Userprofile, bool>> exp =
            u => u.UserFollowers.Any(f => f.StudentId == new UserProfileId(query.StudentRequest.StudentId));
        
        var spec = new UserProfilesSpecification<UserProfileDto>
        (
            new UserProfileSpecParams
            (true, true, ["category", "userFollows"], exp)
        );

        var instructors = await unitOfWork.UserProfileRepository
            .ListAsync(spec, cancellationToken);

        var count = await unitOfWork.UserProfileRepository.CountAsync(new CountSpecification(exp));

        return new GetFollowingInstructorsByIdResult
            (new PaginatedResult<UserProfileDto>(instructors, count, query.StudentRequest.PageNumber, query.StudentRequest.PageSize));
    }
}
