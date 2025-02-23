
namespace Accounting.Application.Accounting.Queries.GetUsers;

public class GetUsersHandler(IAccountingUnitOfWork unitOfWork) : IQueryHandler<GetUsersQuery, GetUsersResult>
{
    public async Task<GetUsersResult> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.UserRepository.GetUsersAsync();

        return new GetUsersResult(users);
    }
}
