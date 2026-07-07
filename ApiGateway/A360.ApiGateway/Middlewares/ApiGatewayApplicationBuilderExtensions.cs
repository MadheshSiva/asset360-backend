namespace A360.ApiGateway.Middlewares;

public static class ApiGatewayApplicationBuilderExtensions
{
    public static WebApplication UseApiGatewayMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("Swagger:Enabled"))
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "A360 API Gateway");
                options.SwaggerEndpoint("/project/swagger/v1/swagger.json", "Project Service");
                options.SwaggerEndpoint("/user-account/swagger/v1/swagger.json", "User Account Service");
                options.SwaggerEndpoint("/people/swagger/v1/swagger.json","People Service");
                options.SwaggerEndpoint("/device/swagger/v1/swagger.json", "Device Service"); 
                options.SwaggerEndpoint("/otmanagement/swagger/v1/swagger.json","OT Management Service");
                options.SwaggerEndpoint("/visitormanagement/swagger/v1/swagger.json","Visitor Management Service");
                options.SwaggerEndpoint("/evacuation/swagger/v1/swagger.json", "Evacuation Service");
                options.SwaggerEndpoint("/media/swagger/v1/swagger.json", "Media Service");

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
