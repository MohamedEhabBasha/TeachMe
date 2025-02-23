using FluentValidation;

namespace UserProfile.Application.UserProfiles.Commands.UpdateUserProfile;

public record UpdateUserProfileCommand(UserProfileDto UserProfileDto) : ICommand<UpdateUserProfileResult>;
public record UpdateUserProfileResult(bool IsSuccess);

public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(u => u.UserProfileDto.Categories)
            .Must(categories => categories.Count <= 3)
            .WithMessage("You can select a maximum of 3 categories.");
    }
}
