using Accounting.Infrastructure;
using Accounting.Application;
using app.API;
using Accounting.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices(builder.Configuration);

builder.Services
    .AddAccountingInfrastructureServices(builder.Configuration)
    .AddAccountingApplicationServices(builder.Configuration);


var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseAccountingDatabaseAsync();
}

app.Run();
