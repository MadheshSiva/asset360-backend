namespace P360.VisitorManagement.Api.Middlewares;

using P360.VisitorManagement.Api.Endpoints;

public static class VisitorManagementApplicationBuilderExtensions
{
    public static WebApplication UseVisitorManagementApiMiddlewares(
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
                service = "VisitorManagement"
            }))
            .WithName("VisitorManagementHealth")
            .WithTags("Health");

        app.MapVisitorManagementEndpoints();
        app.MapVisitorApprovalEndpoints();
        app.MapVisitorEntryExitEndpoints();
        app.MapVisitorRegistrationEndpoints();
        app.MapVisitorIdentificationEndpoints();
        app.MapVisitorReconcilePassEndpoints();
        app.MapVisitorClientPermitEndpoints();
        app.MapVisitorGatePassEndpoints();
        app.MapEmailTemplateEndpoints();

        return app;
    }
}