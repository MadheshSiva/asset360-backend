using P360.UserAccount.Api.Endpoints;

namespace P360.UserAccount.Api.Middlewares;

public static class UserAccountApplicationBuilderExtensions
{
    public static WebApplication UseUserAccountApiMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("Swagger:Enabled"))
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapGet("/health", () => Results.Ok(new { status = "Healthy", service = "UserAccount" }))
            .WithName("UserAccountHealth")
            .WithTags("Health");

        app.MapUserEndpoints();
        app.MapRoleEndpoints();

        return app;
    }
}
