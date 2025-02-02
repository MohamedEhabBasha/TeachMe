using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace Accounting.Infrastructure.Data;

public class AccountingDbContext(DbContextOptions options) : IdentityDbContext<AppUser, AppRole, Guid>(options)
{
    public DbSet<User> AccountUsers => Set<User>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
