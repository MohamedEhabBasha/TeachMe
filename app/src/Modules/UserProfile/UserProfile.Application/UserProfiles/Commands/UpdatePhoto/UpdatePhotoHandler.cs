using Application.Exceptions;
using Application.Services;


namespace UserProfile.Application.UserProfiles.Commands.UpdatePhoto;

public class UpdatePhotoHandler(IUserProfileUnitOfWork unitOfWork, IPhotoService photoService)
    : ICommandHandler<UpdateUserProfilePhotoCommand, UpdateUserProfilePhotoResult>
{
    public async Task<UpdateUserProfilePhotoResult> Handle(UpdateUserProfilePhotoCommand command, CancellationToken cancellationToken)
    {
        var photoDto = command.PhotoDto;

        var userProfile = await unitOfWork.UserProfileRepository.GetUserProfileAsync(photoDto.Id, cancellationToken)
            ?? throw new UserProfileNotFoundException(photoDto.Id);

        if (!string.IsNullOrEmpty(userProfile.Photo.PublicId))
        {
            await photoService.DeletePhotoAsync(userProfile.Photo.PublicId);
        }

        var result = await photoService.AddPhotoAsync(photoDto.File);

        if (result.Error != null) throw new BadRequestException(result.Error.Message);

        var photo = new Photo(result.SecureUrl.AbsoluteUri, result.PublicId);

        userProfile.UpdatePhoto(photo);

        return new UpdateUserProfilePhotoResult(await unitOfWork.CommitAsync(cancellationToken) > 0);
    }
}
