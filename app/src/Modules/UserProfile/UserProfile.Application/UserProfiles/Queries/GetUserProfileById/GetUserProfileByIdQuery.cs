namespace UserProfile.Application.UserProfiles.Queries.GetUserProfileById;

public record GetUserProfileByIdQuery(Guid Id) : IQuery<GetUserProfileByIdResult>;
public record GetUserProfileByIdResult(UserProfileDto UserProfileDto);
