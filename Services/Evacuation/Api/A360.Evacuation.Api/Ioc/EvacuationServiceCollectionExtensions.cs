
using MongoDB.Driver;
using A360.Evacuation.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Settings;

namespace A360.Evacuation.Api.IoC;

public static class EvacuationServiceCollectionExtensions
{
    public static IServiceCollection AddEvacuationApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var mongoDbSettings = new MongoDbSettings
        {
            ConnectionString =
                configuration[$"{MongoDbSettings.SectionName}:ConnectionString"]
                ?? string.Empty,

            DatabaseName =
                configuration[$"{MongoDbSettings.SectionName}:DatabaseName"]
                ?? string.Empty
        };

        mongoDbSettings.Validate();

        services.AddSingleton(mongoDbSettings);

        services.AddSingleton<IMongoClient>(_ =>
            new MongoClient(mongoDbSettings.ConnectionString));

        services.AddSingleton(serviceProvider =>
        {
            var client =
                serviceProvider.GetRequiredService<IMongoClient>();

            return client.GetDatabase(
                mongoDbSettings.DatabaseName);
        });

        // Evacuation Repository Registration

        services.AddScoped<EvacuationRepository>();

        services.AddScoped<IEvacuationRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<EvacuationRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<EvacuationRepository>());

        // Evacuation Trigger Repository Registration

        services.AddScoped<EvacuationTriggerRepository>();

        services.AddScoped<IEvacuationTriggerRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<EvacuationTriggerRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<EvacuationTriggerRepository>());

        // Mongo Index Hosted Service

        services.AddHostedService<MongoIndexHostedService>();

        // Swagger

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "v1",
                new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "A360 Evacuation API",
                    Version = "v1"
                });
        });

        return services;
    }
}
