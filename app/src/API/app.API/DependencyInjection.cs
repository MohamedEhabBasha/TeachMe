using Application.Exceptions.Handler;
using Application.Services;
using Infrastructure.Interceptors;
using Infrastructure.Services.PhotoService;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace app.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices
    (this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddControllers();

        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

        services.AddScoped<IPhotoService, PhotoService>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
            .WithOrigins("http://localhost:4200", "https://localhost:4200"));

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.UseExceptionHandler(options => { });

        return app;
    }
}
