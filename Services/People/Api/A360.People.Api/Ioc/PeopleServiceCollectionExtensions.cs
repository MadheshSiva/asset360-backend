using MongoDB.Driver;
using A360.Email;
using A360.People.Api.Settings;
using A360.People.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Settings;

namespace A360.People.Api.IoC;

public static class PeopleServiceCollectionExtensions
{
    public static IServiceCollection AddPeopleApiServices(
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

        services.AddEmailServices(configuration);

        var visitorNotificationSettings = new VisitorNotificationSettings
        {
            PortalUrl = configuration[$"{VisitorNotificationSettings.SectionName}:PortalUrl"] ?? string.Empty
        };

        visitorNotificationSettings.Validate();

        services.AddSingleton(visitorNotificationSettings);

        // Employee Repository Registration

        services.AddScoped<EmployeeRepository>();

        services.AddScoped<IEmployeeRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<EmployeeRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<EmployeeRepository>());

        // Contractor Repository Registration

        services.AddScoped<ContractorRepository>();

        services.AddScoped<IContractorRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<ContractorRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<ContractorRepository>());

        // Visitor Repository Registration

        services.AddScoped<VisitorRepository>();

        services.AddScoped<IVisitorRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<VisitorRepository>());

        services.AddScoped<PersonalVisionGroupRepository>();

        services.AddScoped<IPersonalVisionGroupRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<PersonalVisionGroupRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<PersonalVisionGroupRepository>());    


        
        services.AddScoped<PersonalVisionAccessRepository>();

        services.AddScoped<IPersonalVisionAccessRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<PersonalVisionAccessRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<PersonalVisionAccessRepository>());      


        services.AddScoped<PersonalWorkScheduleRepository>();

        services.AddScoped<IPersonalWorkScheduleRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<PersonalWorkScheduleRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<PersonalWorkScheduleRepository>());  


         services.AddScoped<PersonalVisionManualAttendanceRepository>();

        services.AddScoped<IPersonalVisionManualAttendanceRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<PersonalVisionManualAttendanceRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<PersonalVisionManualAttendanceRepository>());    


          services.AddScoped<PersonalVisionGreetingsIndividualRepository>();

        services.AddScoped<IPersonalVisionGreetingsIndividualRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<PersonalVisionGreetingsIndividualRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<PersonalVisionGreetingsIndividualRepository>());  
        

         services.AddScoped<PersonalVisionGreetingsGroupsRepository>();

        services.AddScoped<IPersonalVisionGreetingsGroupsRepository>(
            serviceProvider =>
                serviceProvider.GetRequiredService<PersonalVisionGreetingsGroupsRepository>());

        services.AddScoped<IMongoIndexConfigurator>(
            serviceProvider =>
                serviceProvider.GetRequiredService<PersonalVisionGreetingsGroupsRepository
                >());  
         
         services.AddScoped<GroupRepository>();

services.AddScoped<IGroupRepository>(
    serviceProvider =>
        serviceProvider.GetRequiredService<GroupRepository>());

services.AddScoped<IMongoIndexConfigurator>(
    serviceProvider =>
        serviceProvider.GetRequiredService<GroupRepository>());

         services.AddScoped<AccessRepository>();

services.AddScoped<IAccessRepository>(
    serviceProvider =>
        serviceProvider.GetRequiredService<AccessRepository>());

services.AddScoped<IMongoIndexConfigurator>(
    serviceProvider =>
        serviceProvider.GetRequiredService<AccessRepository>());





        // Mongo Index Hosted Service

        services.AddHostedService<MongoIndexHostedService>();

        // Swagger

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}