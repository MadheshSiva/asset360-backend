namespace P360.Media.Api.Services;

public interface IMediaFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file, string category, CancellationToken cancellationToken = default);
}
