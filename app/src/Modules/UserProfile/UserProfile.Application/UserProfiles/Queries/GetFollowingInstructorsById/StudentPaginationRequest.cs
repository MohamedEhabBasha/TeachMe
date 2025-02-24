namespace UserProfile.Application.UserProfiles.Queries.GetFollowingInstructorsById;

public record StudentPaginationRequest(Guid StudentId) : PaginationRequest;
