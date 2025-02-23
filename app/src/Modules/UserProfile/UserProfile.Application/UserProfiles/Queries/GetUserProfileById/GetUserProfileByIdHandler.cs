namespace UserProfile.Application.UserProfiles.Queries.GetUserProfileById;

public class GetUserProfileByIdHandler(IUserProfileUnitOfWork unitOfWork, IMapper mapper) : IQueryHandler<GetUserProfileByIdQuery, GetUserProfileByIdResult>
{
    public async Task<GetUserProfileByIdResult> Handle(GetUserProfileByIdQuery query, CancellationToken cancellationToken)
    {
        var userProfile = await unitOfWork.UserProfileRepository
            .GetUserProfileWithCategoryAsync(new UserProfileId(query.Id), cancellationToken)
            ?? throw new UserProfileNotFoundException(query.Id);

        return new GetUserProfileByIdResult(mapper.Map<UserProfileDto>(userProfile));
    }
}
