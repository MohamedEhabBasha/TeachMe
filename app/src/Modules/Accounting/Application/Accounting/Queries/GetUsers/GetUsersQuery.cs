namespace Accounting.Application.Accounting.Queries.GetUsers;

public record GetUsersQuery() : IQuery<GetUsersResult>;
public record GetUsersResult(IReadOnlyCollection<UserDto> Users);

