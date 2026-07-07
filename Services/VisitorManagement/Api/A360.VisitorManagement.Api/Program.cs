using A360.VisitorManagement.Api.IoC;
using A360.VisitorManagement.Api.Middlewares;

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

builder.Services.AddVisitorManagementApiServices(
    builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAll");

app.UseVisitorManagementApiMiddlewares();

await app.RunAsync();