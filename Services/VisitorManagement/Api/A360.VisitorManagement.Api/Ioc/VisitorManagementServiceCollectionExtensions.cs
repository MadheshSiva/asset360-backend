using MongoDB.Driver;
using A360.Email;
using A360.Media.Client;
using A360.Repository.Repositories;
using A360.Repository.Settings;
using A360.VisitorManagement.Api.Services;
using A360.VisitorManagement.Api.Settings;
using A360.VisitorManagement.Repository.Repositories;

namespace A360.VisitorManagement.Api.IoC;

public static class VisitorManagementServiceCollectionExtensions
{
    public static IServiceCollection AddVisitorManagementApiServices(
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

        // Visitor Management Repository Registration

        services.AddScoped<VisitorManagementRepository>();

        services.AddScoped<IVisitorManagementRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorManagementRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorManagementRepository>());

        // Visitor Approval Repository Registration

        services.AddScoped<VisitorApprovalRepository>();

        services.AddScoped<IVisitorApprovalRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorApprovalRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorApprovalRepository>());

        // Visitor Entry/Exit Repository Registration

        services.AddScoped<VisitorEntryExitRepository>();

        services.AddScoped<IVisitorEntryExitRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorEntryExitRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorEntryExitRepository>());

        // Visitor Registration Repository Registration

        services.AddScoped<VisitorRegistrationRepository>();

        services.AddScoped<IVisitorRegistrationRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorRegistrationRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorRegistrationRepository>());

        // Visitor Identification Repository Registration

        services.AddScoped<VisitorIdentificationRepository>();

        services.AddScoped<IVisitorIdentificationRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorIdentificationRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorIdentificationRepository>());

        // Visitor Reconcile Pass Repository Registration

        services.AddScoped<VisitorReconcilePassRepository>();

        services.AddScoped<IVisitorReconcilePassRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorReconcilePassRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorReconcilePassRepository>());

        // Visitor Client Permit Repository Registration

        services.AddScoped<VisitorClientPermitRepository>();

        services.AddScoped<IVisitorClientPermitRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorClientPermitRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorClientPermitRepository>());

        // Visitor Gate Pass Repository Registration

        services.AddScoped<VisitorGatePassRepository>();

        services.AddScoped<IVisitorGatePassRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorGatePassRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorGatePassRepository>());

        // Email Template Repository Registration

        services.AddScoped<EmailTemplateRepository>();

        services.AddScoped<IEmailTemplateRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<EmailTemplateRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<EmailTemplateRepository>());

        // Mongo Index Hosted Service

        services.AddHostedService<MongoIndexHostedService>();

        // File Storage

        services.AddMediaStorageClient(configuration);
        services.AddSingleton<IVisitorPanelFileStorageService, VisitorPanelFileStorageService>();

        // Email

        services.AddEmailServices(configuration);

        var gatePassNotificationSettings = new GatePassNotificationSettings
        {
            PortalUrl =
                configuration[$"{GatePassNotificationSettings.SectionName}:PortalUrl"]
                ?? string.Empty,

            ApiBaseUrl =
                configuration[$"{GatePassNotificationSettings.SectionName}:ApiBaseUrl"]
                ?? string.Empty,

            LinkSecret =
                configuration[$"{GatePassNotificationSettings.SectionName}:LinkSecret"]
                ?? string.Empty
        };

        gatePassNotificationSettings.Validate();

        services.AddSingleton(gatePassNotificationSettings);

        // Swagger

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}