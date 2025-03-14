namespace UserProfile.Application.UserProfiles.Queries.GetUserProfileById;

public class GetUserProfileByIdHandler(IUserProfileUnitOfWork unitOfWork, IMapper mapper) : IQueryHandler<GetUserProfileByIdQuery, GetUserProfileByIdResult>
{
    public async Task<GetUserProfileByIdResult> Handle(GetUserProfileByIdQuery query, CancellationToken cancellationToken)
    {
        var spec = new UserProfilesSpecification
            (
                new UserProfileSpecParams(false, false, ["category", "userFollows"], u => u.Id == new UserProfileId(query.Id))
            );

        var userProfile = await unitOfWork.UserProfileRepository
            .GetEntityWithSpec(spec, cancellationToken)
            ?? throw new UserProfileNotFoundException(query.Id);

        UserProfileDto profileDto = mapper.Map<UserProfileDto>(userProfile);

        return new GetUserProfileByIdResult(profileDto);
    }
}
