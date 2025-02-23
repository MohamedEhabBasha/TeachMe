using FluentValidation;

namespace UserProfile.Application.UserProfiles.Commands.UpdateFollowStatus;

public record UpdateFollowStatusCommand(FollowDto FollowDto) : ICommand<UpdateFollowStatusResult>;
public record UpdateFollowStatusResult(bool IsSuccess);

public class UpdateFollowStatusCommandValidator : AbstractValidator<UpdateFollowStatusCommand>
{
    public UpdateFollowStatusCommandValidator()
    {
        RuleFor(u => u.FollowDto.InstructorId).NotNull().NotEmpty()
            .WithMessage("Instructor Id Is null or empty!!");

       RuleFor(u => u.FollowDto.StudentId).NotNull().NotEmpty()
            .WithMessage("Student Id Is null or empty!!");
    }
}