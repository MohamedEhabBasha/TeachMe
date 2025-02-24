namespace app.API.Modules.Accounting;

public record RegisterNewUserRequest(RegisterDto RegisterDto);
public record RegisterNewUserResponse(UserDto UserDto);
public record LoginRequest(LoginDto LoginDto);
public record LoginResponse(UserDto UserDto);

//public record GetUsersRequest(PaginationRequest PaginationRequest);
public record GetUsersResponse(PaginatedResult<UserDto> Users);