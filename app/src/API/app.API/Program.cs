using Accounting.Infrastructure;
using Accounting.Application;
using Accounting.Infrastructure.Data.Extensions;

using UserProfile.Infrastructure;

using app.API;
using UserProfile.Application;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices(builder.Configuration);

// Accounting Module
builder.Services
    .AddAccountingInfrastructureServices(builder.Configuration)
    .AddAccountingApplicationServices(builder.Configuration);

// UserProfile Module
builder.Services   
    .AddUserProfileInfrastructureServices(builder.Configuration)
    .AddUserProfileApplicationServices(builder.Configuration);


var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseAccountingDatabaseAsync();
}

app.Run();
