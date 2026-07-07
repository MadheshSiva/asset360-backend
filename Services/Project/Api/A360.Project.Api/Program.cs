using A360.Project.Api.IoC;
using A360.Project.Api.Middlewares;

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

builder.Services.AddProjectApiServices(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAll");

app.UseProjectApiMiddlewares();

await app.RunAsync();