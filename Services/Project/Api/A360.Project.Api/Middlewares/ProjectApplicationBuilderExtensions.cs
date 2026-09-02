using A360.Project.Api.Endpoints;

namespace A360.Project.Api.Middlewares;

public static class ProjectApplicationBuilderExtensions
{
    public static WebApplication UseProjectApiMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("Swagger:Enabled"))
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapGet("/health", () => Results.Ok(new { status = "Healthy", service = "Project" }))
            .WithName("ProjectHealth")
            .WithTags("Health");

        app.MapProjectEndpoints();
        app.MapCountryEndpoints();
        app.MapAreaEndpoints();
        app.MapOuterZoneEndpoints();
        app.MapBuildingEndpoints();
        app.MapFloorEndpoints();
        app.MapZoneEndpoints();
        app.MapSubZoneEndpoints();
        app.MapZoneMappingEndpoints();
        app.MapDeviceZoneMappingEndpoints();

        return app;
    }
}
