namespace UserProfile.Application.UserProfiles.Queries.GetUserProfiles;

public record GetUserProfilesQuery(string? Category, string? Search, bool MostFollowed) : PaginationRequest, IQuery<GetUserProfilesResult>;
public record GetUserProfilesResult(PaginatedResult<UserProfileDto> Users);