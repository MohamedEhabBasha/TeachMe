using Accounting.Application.Accounting;
using Accounting.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Accounting.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountingInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AccountingDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Accounting.Infrastructure"));
        });

        services.AddIdentityCore<AppUser>(opt =>
        {

        })
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddEntityFrameworkStores<AccountingDbContext>();

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountingUnitOfWork, AccountingUnitOfWork>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var tokenKey = configuration["TokenKey"] ?? throw new Exception("TokenKey not found");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return services;
    }
}
