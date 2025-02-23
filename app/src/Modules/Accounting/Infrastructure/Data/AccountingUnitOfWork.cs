using Accounting.Application.Accounting;

namespace Accounting.Infrastructure.Data;

public class AccountingUnitOfWork(AccountingDbContext context, IUserRepository userRepository) : IAccountingUnitOfWork
{
    public IUserRepository UserRepository => userRepository;

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}
