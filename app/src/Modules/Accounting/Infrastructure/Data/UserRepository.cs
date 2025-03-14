using Accounting.Application.Accounting;
using Accounting.Application.Dtos;
using Accounting.Infrastructure.Services;
using Application.Pagination;

namespace Accounting.Infrastructure.Data;

public class UserRepository
    (AccountingDbContext context, UserManager<AppUser> userManager, IAuthenticationService authorizationService) 
    : IUserRepository
{
    public async Task<UserDto> Register(RegisterDto registerDto)
    {
        if (await userManager.FindByNameAsync(registerDto.UserName) is not null)
            throw new BadRequestException("Username is already registered !");

        var appUser = new AppUser
        {
            UserName = registerDto.UserName,
        };

        var result = await userManager.CreateAsync(appUser, registerDto.Password);

        if (!result.Succeeded)
            throw new BadRequestException(result.Errors.ToString()!);

        await userManager.AddToRoleAsync(appUser, registerDto.Role);

        var user = CreateUser(registerDto, appUser.Id);

        await context.AccountUsers.AddAsync(user);

       return await ToUserDto(user, appUser);
    }

    public async Task<UserDto> Login(LoginDto loginDto)
    {
        var appUser = await userManager.Users
            .FirstOrDefaultAsync(x => x.NormalizedUserName == loginDto.UserName.ToUpper());

        if (appUser is null || !await userManager.CheckPasswordAsync(appUser, loginDto.Password))
        {
            throw new BadRequestException("Wrong UserName or Password");
        }

        var user = await context.AccountUsers.FirstOrDefaultAsync(a => a.Id == new UserId(appUser.Id))
            ?? throw new NotFoundException("User doesn't exist!!");

        return await ToUserDto(user, appUser);
    }
    public async Task<PaginatedResult<UserDto>> GetUsersAsync(PaginationRequest paginationRequest)
    {
        var pageNumber = paginationRequest.PageNumber;
        var pageSize =  paginationRequest.PageSize;

        var query = context.AccountUsers
            .AsQueryable()
            .AsNoTracking()
            .Select(u => ToUserDtoWithNoJwt(u));

        return await PaginatedResult<UserDto>.CreateAsync(query, pageNumber, pageSize);
    }

    private async Task<UserDto> ToUserDto(User user, AppUser appUser)
    {
        return new UserDto(user.Id.Value, user.Name, user.UserName, await authorizationService.CreateJwtToken(appUser), user.Role.Name);
    }
    private static UserDto ToUserDtoWithNoJwt(User user)
    {
        return new UserDto(user.Id.Value, user.Name, user.UserName, "", user.Role.Name);
    }

    private static User CreateUser(RegisterDto registerDto, Guid id)
    {
        if (registerDto.Role == "Student")
            return User.CreateStudent(id, registerDto.Name, registerDto.UserName);
        else if (registerDto.Role == "Instructor")
            return User.CreateInstructor(id, registerDto.Name, registerDto.UserName);

        throw new Exception("Role is not correct");
    }
}
