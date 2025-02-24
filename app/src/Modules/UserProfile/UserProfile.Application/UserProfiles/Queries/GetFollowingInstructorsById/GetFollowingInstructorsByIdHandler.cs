namespace UserProfile.Application.UserProfiles.Queries.GetFollowingInstructorsById;

public class GetFollowingInstructorsByIdHandler(IUserProfileUnitOfWork unitOfWork, IMapper mapper)
    : IQueryHandler<GetFollowingInstructorsByIdQuery, GetFollowingInstructorsByIdResult>
{
    public async Task<GetFollowingInstructorsByIdResult> Handle(GetFollowingInstructorsByIdQuery query, CancellationToken cancellationToken)
    {
        var instructors = await unitOfWork.UserProfileRepository
            .GetFollowingInstructorsByIdAsync(query.StudentRequest, cancellationToken);

        return new GetFollowingInstructorsByIdResult(instructors);
    }
}
