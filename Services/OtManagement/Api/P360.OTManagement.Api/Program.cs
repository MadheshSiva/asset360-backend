using P360.OTManagement.Api.IoC;
using P360.OTManagement.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddOTManagementApiServices(
    builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAll");

app.UseOTManagementApiMiddlewares();

await app.RunAsync();