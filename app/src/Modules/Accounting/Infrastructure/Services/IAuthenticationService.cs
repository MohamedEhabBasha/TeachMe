namespace Accounting.Infrastructure.Services;

public interface IAuthenticationService
{
    Task<string> CreateJwtToken(AppUser user);
}
