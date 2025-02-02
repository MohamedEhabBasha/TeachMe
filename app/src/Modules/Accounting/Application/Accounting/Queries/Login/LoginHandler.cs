
namespace Accounting.Application.Accounting.Queries.Login;

public class LoginHandler(IUserRepository userRepository) : IQueryHandler<LoginQuery, LoginResult>
{
    public async Task<LoginResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var userDto = await userRepository.LoginAsync(query.LoginDto);

        return new LoginResult(userDto);
    }
}
