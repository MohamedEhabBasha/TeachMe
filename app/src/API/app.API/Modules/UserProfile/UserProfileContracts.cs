using UserProfile.Application.Dtos;

namespace app.API.Modules.UserProfile;

public record UpdateUserProfileRequest(UserProfileDto UserProfileDto);
public record UpdateUserProfileResponse(bool IsSuccess);
/**------------------------------------------------------------------------**/
public record UpdateUserProfilePhotoRequest(PhotoDto PhotoDto);
public record UpdateUserProfilePhotoResponse(bool IsSuccess);
/**------------------------------------------------------------------------**/
public record UpdateProfileFollowStatusRequest(FollowDto FollowDto);
public record UpdateProfileFollowStatusResponse(bool IsSuccess);
/**------------------------------------------------------------------------**/
public record GetUserProfileByIdRequest(Guid Id);
public record GetUserProfileByIdResponse(UserProfileDto UserProfileDto);
/**------------------------------------------------------------------------**/
public record GetFollowingInstructorsByIdRequest(Guid StudentId);
public record GetFollowingInstructorsByIdResponse(IReadOnlyCollection<UserProfileDto> UserProfiles);
