
using P360.Devices.Api.IoC;
using P360.Devices.Api.Middlewares;

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

builder.Services.AddDeviceApiServices(
    builder.Configuration);


var app = builder.Build();

app.UseCors("AllowAll");

app.UseDeviceApiMiddlewares();

await app.RunAsync();
