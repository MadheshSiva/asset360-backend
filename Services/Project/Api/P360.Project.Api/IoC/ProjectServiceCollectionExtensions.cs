using MongoDB.Driver;
using P360.Media.Client;
using P360.Project.Api.Services;
using P360.Project.Repository.Repositories;
using P360.Repository.Repositories;
using P360.Repository.Settings;

namespace P360.Project.Api.IoC;

public static class ProjectServiceCollectionExtensions
{
    public static IServiceCollection AddProjectApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var mongoDbSettings = new MongoDbSettings
        {
            ConnectionString = configuration[$"{MongoDbSettings.SectionName}:ConnectionString"] ?? string.Empty,
            DatabaseName = configuration[$"{MongoDbSettings.SectionName}:DatabaseName"] ?? string.Empty
        };

        mongoDbSettings.Validate();

        services.AddSingleton(mongoDbSettings);
        services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoDbSettings.ConnectionString));
        services.AddSingleton(serviceProvider =>
        {
            var client = serviceProvider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(mongoDbSettings.DatabaseName);
        });

        services.AddScoped<ProjectRepository>();
        services.AddScoped<IProjectRepository>(serviceProvider => serviceProvider.GetRequiredService<ProjectRepository>());
        services.AddScoped<IMongoIndexConfigurator>(serviceProvider => serviceProvider.GetRequiredService<ProjectRepository>());

        services.AddScoped<CountryRepository>();
        services.AddScoped<ICountryRepository>(serviceProvider => serviceProvider.GetRequiredService<CountryRepository>());
        services.AddScoped<IMongoIndexConfigurator>(serviceProvider => serviceProvider.GetRequiredService<CountryRepository>());

        services.AddScoped<AreaRepository>();
        services.AddScoped<IAreaRepository>(serviceProvider => serviceProvider.GetRequiredService<AreaRepository>());
        services.AddScoped<IMongoIndexConfigurator>(serviceProvider => serviceProvider.GetRequiredService<AreaRepository>());

        services.AddScoped<BuildingRepository>();
        services.AddScoped<IBuildingRepository>(serviceProvider => serviceProvider.GetRequiredService<BuildingRepository>());
        services.AddScoped<IMongoIndexConfigurator>(serviceProvider => serviceProvider.GetRequiredService<BuildingRepository>());

        services.AddScoped<FloorRepository>();
        services.AddScoped<IFloorRepository>(serviceProvider => serviceProvider.GetRequiredService<FloorRepository>());
        services.AddScoped<IMongoIndexConfigurator>(serviceProvider => serviceProvider.GetRequiredService<FloorRepository>());

        services.AddScoped<ZoneRepository>();
        services.AddScoped<IZoneRepository>(serviceProvider => serviceProvider.GetRequiredService<ZoneRepository>());
        services.AddScoped<IMongoIndexConfigurator>(serviceProvider => serviceProvider.GetRequiredService<ZoneRepository>());

        services.AddScoped<SubZoneRepository>();
        services.AddScoped<ISubZoneRepository>(serviceProvider => serviceProvider.GetRequiredService<SubZoneRepository>());
        services.AddScoped<IMongoIndexConfigurator>(serviceProvider => serviceProvider.GetRequiredService<SubZoneRepository>());

        services.AddScoped<ZoneMappingRepository>();
        services.AddScoped<IZoneMappingRepository>(serviceProvider => serviceProvider.GetRequiredService<ZoneMappingRepository>());
        services.AddScoped<IMongoIndexConfigurator>(serviceProvider => serviceProvider.GetRequiredService<ZoneMappingRepository>());

        services.AddScoped<DeviceZoneMappingRepository>();
        services.AddScoped<IDeviceZoneMappingRepository>(serviceProvider => serviceProvider.GetRequiredService<DeviceZoneMappingRepository>());
        services.AddScoped<IMongoIndexConfigurator>(serviceProvider => serviceProvider.GetRequiredService<DeviceZoneMappingRepository>());

        services.AddHostedService<MongoIndexHostedService>();
        services.AddMediaStorageClient(configuration);
        services.AddSingleton<IMapFileStorageService, MapFileStorageService>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
