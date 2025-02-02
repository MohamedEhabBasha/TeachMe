namespace Accounting.Application.Accounting.Queries.Login;

public record LoginQuery(LoginDto LoginDto) : IQuery<LoginResult>;
public record LoginResult(UserDto UserDto);
