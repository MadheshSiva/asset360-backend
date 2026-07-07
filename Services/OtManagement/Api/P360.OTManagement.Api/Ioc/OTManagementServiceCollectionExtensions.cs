using MongoDB.Driver;
using P360.OTManagement.Repository.Repositories;
using P360.Repository.Repositories;
using P360.Repository.Settings;

namespace P360.OTManagement.Api.IoC;

public static class OTManagementServiceCollectionExtensions
{
    public static IServiceCollection AddOTManagementApiServices(
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

        // OT Management Repository Registration

        services.AddScoped<OTManagementRepository>();

        services.AddScoped<IOTManagementRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<OTManagementRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<OTManagementRepository>());

        // Equipment Master Repository Registration

        services.AddScoped<EquipmentMasterRepository>();

        services.AddScoped<IEquipmentMasterRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<EquipmentMasterRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<EquipmentMasterRepository>());


            services.AddScoped<StaffManagementRepository>();

        services.AddScoped<IStaffManagementRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<StaffManagementRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<StaffManagementRepository>());      


               
            services.AddScoped<PatientMasterRepository>();

        services.AddScoped<IPatientMasterRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<PatientMasterRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<PatientMasterRepository>());       


              services.AddScoped<OTSchedulingRepository>();

        services.AddScoped<IOTSchedulingRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<OTSchedulingRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<OTSchedulingRepository>());       
   

        // Mongo Index Hosted Service

        services.AddHostedService<MongoIndexHostedService>();

        // Swagger

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}