
using MongoDB.Driver;
using P360.Devices.Repository.Repositories;
using P360.Repository.Repositories;
using P360.Repository.Settings;

namespace P360.Devices.Api.IoC;

public static class DeviceServiceCollectionExtensions
{
    public static IServiceCollection AddDeviceApiServices(
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

        // Device Repository Registration

        services.AddScoped<DeviceRepository>();

        services.AddScoped<IDeviceRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<DeviceRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<DeviceRepository>());

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
                    Title = "P360 Devices API",
                    Version = "v1"
                });
        });

        return services;
    }
}

