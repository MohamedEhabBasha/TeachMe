namespace Accounting.Application.Accounting;

public interface IUserRepository
{
    Task<UserDto> Register(RegisterDto registerDto);
    Task<UserDto> Login(LoginDto loginDto);
    Task<PaginatedResult<UserDto>> GetUsersAsync(PaginationRequest paginationRequest);
}
