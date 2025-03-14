using Application.Exceptions;
using Application.Services;

namespace UserProfile.Application.UserProfiles.Commands.UpdatePhoto;

public class UpdatePhotoHandler(IUserProfileUnitOfWork unitOfWork, IPhotoService photoService)
    : ICommandHandler<UpdateUserProfilePhotoCommand, UpdateUserProfilePhotoResult>
{
    public async Task<UpdateUserProfilePhotoResult> Handle(UpdateUserProfilePhotoCommand command, CancellationToken cancellationToken)
    {
        var spec = new UserProfilesSpecification(new UserProfileSpecParams(false, false, [], u => u.Id == new UserProfileId(command.UserId)));

        var userProfile = await unitOfWork.UserProfileRepository.GetEntityWithSpec(spec, cancellationToken)
            ?? throw new UserProfileNotFoundException(command.UserId);

        if (!string.IsNullOrEmpty(userProfile.Photo.PublicId))
        {
            await photoService.DeletePhotoAsync(userProfile.Photo.PublicId);
        }

        var result = await photoService.AddPhotoAsync(command.File);

        if (result.Error != null) throw new BadRequestException(result.Error.Message);

        var photo = new Photo(result.SecureUrl.AbsoluteUri, result.PublicId);

        userProfile.UpdatePhoto(photo);

        if (await unitOfWork.CommitAsync(cancellationToken) > 0)
            return new UpdateUserProfilePhotoResult(new (photo.Url!, photo.PublicId!));

        throw new BadRequestException("Problem while uploading the photo!!");
    }
}
