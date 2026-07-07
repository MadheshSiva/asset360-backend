using A360.People.Api.IoC;
using A360.People.Api.Middlewares;

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


builder.Services.AddPeopleApiServices(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAll");


app.UsePeopleApiMiddlewares();

await app.RunAsync();