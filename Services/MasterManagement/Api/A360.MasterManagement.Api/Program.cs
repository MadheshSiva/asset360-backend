using A360.MasterManagement.Api.IoC;
using A360.MasterManagement.Api.Middlewares;

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

builder.Services.AddMasterManagementApiServices(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAll");

app.UseMasterManagementApiMiddlewares();

await app.RunAsync();
