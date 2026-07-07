using P360.Media.Api.Services;

namespace P360.Media.Api.IoC;

public static class MediaServiceCollectionExtensions
{
    public static IServiceCollection AddMediaApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IMediaFileStorageService, MediaFileStorageService>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
