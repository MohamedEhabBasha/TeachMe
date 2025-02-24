namespace UserProfile.Application.UserProfiles.Queries.GetUserProfileById;

public class GetUserProfileByIdHandler(IUserProfileUnitOfWork unitOfWork, IMapper mapper) : IQueryHandler<GetUserProfileByIdQuery, GetUserProfileByIdResult>
{
    public async Task<GetUserProfileByIdResult> Handle(GetUserProfileByIdQuery query, CancellationToken cancellationToken)
    {
        var userProfile = await unitOfWork.UserProfileRepository
            .GetUserProfileWithCategoryAsync(new UserProfileId(query.Id), cancellationToken)
            ?? throw new UserProfileNotFoundException(query.Id);

        UserProfileDto profileDto = mapper.Map<UserProfileDto>(userProfile);

        var count = await unitOfWork.UserProfileRepository.GetFollowersCountByIdAsync(query.Id, cancellationToken);

        profileDto = profileDto with { UserFollowsCount = count };

        return new GetUserProfileByIdResult(profileDto);
    }
}
