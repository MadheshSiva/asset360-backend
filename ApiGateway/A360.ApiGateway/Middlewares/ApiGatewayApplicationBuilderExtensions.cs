using A360.ApiGateway.Swagger;
using Microsoft.Extensions.Caching.Memory;

namespace A360.ApiGateway.Middlewares;

public static class ApiGatewayApplicationBuilderExtensions
{
    private const string MergedSwaggerCacheKey = "MergedSwaggerDocument";

    public static WebApplication UseApiGatewayMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("Swagger:Enabled"))
        {
            app.UseSwagger();

            app.MapGet("/openapi/all.json", async (
                IConfiguration configuration,
                IHttpClientFactory httpClientFactory,
                IMemoryCache cache,
                ILoggerFactory loggerFactory,
                CancellationToken cancellationToken) =>
            {
                if (cache.TryGetValue(MergedSwaggerCacheKey, out string? cached) && cached is not null)
                {
                    return Results.Content(cached, "application/json");
                }

                var sources = SwaggerAggregator.GetSources(configuration);
                var merged = await SwaggerAggregator.BuildMergedDocumentAsync(
                    sources,
                    httpClientFactory,
                    loggerFactory.CreateLogger("SwaggerAggregator"),
                    cancellationToken);

                var json = merged.ToJsonString();
                cache.Set(MergedSwaggerCacheKey, json, TimeSpan.FromSeconds(30));

                return Results.Content(json, "application/json");
            })
                .WithName("MergedSwagger")
                .WithTags("Gateway")
                .ExcludeFromDescription();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/all.json", "All Services (Aggregated)");
                options.SwaggerEndpoint("/project/swagger/v1/swagger.json", "Project Service");
                options.SwaggerEndpoint("/user-account/swagger/v1/swagger.json", "User Account Service");
                options.SwaggerEndpoint("/people/swagger/v1/swagger.json", "People Service");
                options.SwaggerEndpoint("/device/swagger/v1/swagger.json", "Device Service");
                options.SwaggerEndpoint("/otmanagement/swagger/v1/swagger.json", "OT Management Service");
                options.SwaggerEndpoint("/visitormanagement/swagger/v1/swagger.json", "Visitor Management Service");
                options.SwaggerEndpoint("/evacuation/swagger/v1/swagger.json", "Evacuation Service");
                options.SwaggerEndpoint("/media/swagger/v1/swagger.json", "Media Service");
                options.SwaggerEndpoint("/asset/swagger/v1/swagger.json", "Asset Service");
                options.SwaggerEndpoint("/master-management/swagger/v1/swagger.json", "Master Management Service");

                options.RoutePrefix = "swagger";
            });
        }

        app.MapGet("/gateway/health", () => Results.Ok(new { status = "Healthy", service = "ApiGateway" }))
            .WithName("GatewayHealth")
            .WithTags("Gateway");

        app.MapGet("/gateway/swagger/downstream", () => Results.Ok(new[]
            {
                new
                {
                    service = "Project",
                    gatewaySwaggerUrl = "/project/swagger/v1/swagger.json",
                    serviceBaseUrl = "http://172.16.100.26:5254",
                    healthUrl = "/project/health"
                },
                new
                {
                    service = "UserAccount",
                    gatewaySwaggerUrl = "/user-account/swagger/v1/swagger.json",
                    serviceBaseUrl = "http://172.16.100.26:5018",
                    healthUrl = "/user-account/health"
                },
                new
               {
                    service = "People",
                    gatewaySwaggerUrl ="/people/swagger/v1/swagger.json",
                    serviceBaseUrl ="http://172.16.100.26:5055",
                    healthUrl ="/people/health"
               },
                new
               {
                    service = "Device",
                    gatewaySwaggerUrl = "/device/swagger/v1/swagger.json",
                    serviceBaseUrl = "http://172.16.100.26:5077",
                    healthUrl = "/device/health"
               },
                new
               {
                    service = "OTManagement",
                    gatewaySwaggerUrl = "/otmanagement/swagger/v1/swagger.json",
                    serviceBaseUrl = "http://172.16.100.26:5123",
                    healthUrl = "/otmanagement/health"
               },
                new
               {
                    service = "VisitorManagement",
                    gatewaySwaggerUrl = "/visitormanagement/swagger/v1/swagger.json",
                    serviceBaseUrl = "http://172.16.100.26:5125",
                    healthUrl = "/visitormanagement/health"
               },
                new
               {
                    service = "Evacuation",
                    gatewaySwaggerUrl = "/evacuation/swagger/v1/swagger.json",
                    serviceBaseUrl = "http://172.16.100.26:5140",
                    healthUrl = "/evacuation/health"
               },
                new
               {
                    service = "Media",
                    gatewaySwaggerUrl = "/media/swagger/v1/swagger.json",
                    serviceBaseUrl = "http://172.16.100.26:5300",
                    healthUrl = "/media/health"
               }
            }))
            .WithName("GatewayDownstreamSwagger")
            .WithTags("Gateway");

        app.MapReverseProxy();

        return app;
    }
}
