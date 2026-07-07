
namespace A360.Evacuation.Api.Middlewares;

using A360.Evacuation.Api.Endpoints;

public static class EvacuationApplicationBuilderExtensions
{
    public static WebApplication UseEvacuationApiMiddlewares(
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
                service = "Evacuation"
            }))
            .WithName("EvacuationHealth")
            .WithTags("Health");

        app.MapEvacuationEndpoints();

        app.MapEvacuationTriggerEndpoints();

        return app;
    }
}
