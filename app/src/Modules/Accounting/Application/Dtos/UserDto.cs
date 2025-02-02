namespace Accounting.Application.Dtos;

public record UserDto(Guid Id, string Name, string UserName, string Token, string Role);
