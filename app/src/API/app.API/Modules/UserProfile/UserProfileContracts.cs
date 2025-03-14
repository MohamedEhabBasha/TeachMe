using Application.CQRS;
using UserProfile.Application.Dtos;
using UserProfile.Application.UserProfiles.Queries.GetFollowingInstructorsById;

namespace app.API.Modules.UserProfile;

public record UpdateUserProfileRequest(UserProfileDto UserProfileDto);
public record UpdateUserProfileResponse(bool IsSuccess);
/**------------------------------------------------------------------------**/
public record UpdateUserProfilePhotoRequest(Guid UserId, IFormFile File);
public record UpdateUserProfilePhotoResponse(PhotoDto PhotoDto);
/**------------------------------------------------------------------------**/
public record UpdateProfileFollowStatusRequest(FollowDto FollowDto);
public record UpdateProfileFollowStatusResponse(bool IsSuccess);
/**------------------------------------------------------------------------**/
public record GetUserProfileByIdRequest(Guid Id);
public record GetUserProfileByIdResponse(UserProfileDto UserProfileDto);
/**------------------------------------------------------------------------**/
public record GetFollowingInstructorsByIdRequest(StudentPaginationRequest StudentRequest);
public record GetFollowingInstructorsByIdResponse(PaginatedResult<UserProfileDto> UserProfiles);
/**------------------------------------------------------------------------**/
public record GetUserProfilesRequest(string? Category, string? Search, bool MostFollowed) : PaginationRequest;
public record GetUserProfilesResponse(PaginatedResult<UserProfileDto> Users);
