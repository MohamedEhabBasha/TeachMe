namespace Accounting.Application.Accounting.Commands.RegisterNewUser;

public class RegisterNewUserHandler(IAccountingUnitOfWork unitOfWork, IUserRepository userRepository) : ICommandHandler<RegisterNewUserCommand, RegisterNewUserResult>
{
    public async Task<RegisterNewUserResult> Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
    {
        var userDto = await userRepository.AddAsync(command.RegisterDto);

        await unitOfWork.CommitAsync(cancellationToken);

        return new RegisterNewUserResult(userDto);
    }

}
