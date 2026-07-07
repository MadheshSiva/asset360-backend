namespace P360.OTManagement.Api.Middlewares;

using P360.OTManagement.Api.Endpoints;

public static class OTManagementApplicationBuilderExtensions
{
    public static WebApplication UseOTManagementApiMiddlewares(
        this WebApplication app)
    {
        if (app.Environment.IsDevelopment() ||
            app.Configuration.GetValue<bool>("Swagger:Enabled"))
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapGet("/health", () =>
            Results.Ok(new
            {
                status = "Healthy",
                service = "OTManagement"
            }))
            .WithName("OTManagementHealth")
            .WithTags("Health");

        app.MapOTManagementEndpoints();
        app.MapEquipmentMasterEndpoints();
        app.MapStaffManagementEndpoints();
        app.MapPatientMasterEndpoints();
        app.MapOTSchedulingEndpoints();

        return app;
    }
}