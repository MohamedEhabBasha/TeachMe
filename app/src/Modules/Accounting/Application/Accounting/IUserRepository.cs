namespace Accounting.Application.Accounting;

public interface IUserRepository
{
    Task<UserDto> AddAsync(RegisterDto registerDto);
    Task<UserDto> LoginAsync(LoginDto loginDto);
    Task<IReadOnlyCollection<UserDto>> GetUsersAsync();
}
