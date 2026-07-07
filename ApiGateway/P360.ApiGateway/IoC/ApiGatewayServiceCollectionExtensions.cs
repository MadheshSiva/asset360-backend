using Yarp.ReverseProxy;

namespace P360.ApiGateway.IoC;

public static class ApiGatewayServiceCollectionExtensions
{
    public static IServiceCollection AddApiGatewayServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddReverseProxy()
            .LoadFromConfig(configuration.GetSection("ReverseProxy"));

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
