namespace Accounting.Application.Accounting.Commands.RegisterNewUser;

public class RegisterNewUserHandler(IAccountingUnitOfWork unitOfWork) : ICommandHandler<RegisterNewUserCommand, RegisterNewUserResult>
{
    public async Task<RegisterNewUserResult> Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
    {
        var userDto = await unitOfWork.UserRepository.AddAsync(command.RegisterDto);

        await unitOfWork.CommitAsync(cancellationToken);

        return new RegisterNewUserResult(userDto);
    }

}
