using FluentValidation;

namespace Accounting.Application.Accounting.Commands.RegisterNewUser;

public record RegisterNewUserCommand(RegisterDto RegisterDto) : ICommand<RegisterNewUserResult>;
public record RegisterNewUserResult(UserDto UserDto);

public class RegisterNewUserCommandValidator : AbstractValidator<RegisterNewUserCommand>
{
    public RegisterNewUserCommandValidator()
    {
        RuleFor(x => x.RegisterDto.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.RegisterDto.UserName).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.RegisterDto.Password).NotEmpty().MinimumLength(6).WithMessage("Password is not correct");
        RuleFor(x => x.RegisterDto.Role).NotEmpty().WithMessage("Role should not be empty");
    }
}
