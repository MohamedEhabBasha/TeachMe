using Microsoft.AspNetCore.Http;

namespace UserProfile.Application.UserProfiles.Commands.UpdatePhoto;

public record UpdateUserProfilePhotoCommand(Guid UserId, IFormFile File) : ICommand<UpdateUserProfilePhotoResult>;
public record UpdateUserProfilePhotoResult(PhotoDto PhotoDto);
