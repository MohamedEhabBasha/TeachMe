namespace UserProfile.Application.UserProfiles.Commands.UpdatePhoto;

public record UpdateUserProfilePhotoCommand(PhotoDto PhotoDto) : ICommand<UpdateUserProfilePhotoResult>;
public record UpdateUserProfilePhotoResult(bool IsSuccess);
