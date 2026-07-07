
namespace P360.Devices.Api.Middlewares;

using P360.Devices.Api.Endpoints;

public static class DeviceApplicationBuilderExtensions
{
    public static WebApplication UseDeviceApiMiddlewares(
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
                service = "Devices"
            }))
            .WithName("DevicesHealth")
            .WithTags("Health");

        app.MapDeviceEndpoints();

        return app;
    }
}

