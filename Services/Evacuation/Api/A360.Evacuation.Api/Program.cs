
using A360.Evacuation.Api.IoC;
using A360.Evacuation.Api.Middlewares;

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

builder.Services.AddEvacuationApiServices(
    builder.Configuration);


var app = builder.Build();

app.UseCors("AllowAll");

app.UseEvacuationApiMiddlewares();

await app.RunAsync();
