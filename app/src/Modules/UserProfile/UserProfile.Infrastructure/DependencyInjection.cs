using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserProfile.Application.Contracts;
using UserProfile.Infrastructure.Data;

namespace UserProfile.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddUserProfileInfrastructureServices
    (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<UserProfileContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("UserProfile.Infrastructure"));
        });

        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<IUserProfileUnitOfWork, UserProfileUnitOfWork>();

        return services;
    }
}
