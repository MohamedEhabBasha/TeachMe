using Application;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class UnitOfWork(DbContext context) : IUnitOfWork
{
    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}
