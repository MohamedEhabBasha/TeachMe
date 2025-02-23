using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Accounting.Infrastructure.Data.Extensions;

public static class DatabaseExtentions
{
    public static async Task InitialiseAccountingDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<AccountingDbContext>();

            var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

            await context.Database.MigrateAsync();

            await Seed.SeedRoles(roleManager);
        }
        catch (Exception ex)
        {
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("Accounting - Seeding Data");
            logger.LogError(ex, "An error occurred during migration");
        }
    }
}
