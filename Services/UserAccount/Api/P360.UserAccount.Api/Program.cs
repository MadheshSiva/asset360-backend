using P360.UserAccount.Api.IoC;
using P360.UserAccount.Api.Middlewares;

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
builder.Services.AddUserAccountApiServices(builder.Configuration);

var app = builder.Build();
app.UseCors("AllowAll");

app.UseUserAccountApiMiddlewares();
await app.RunAsync();
