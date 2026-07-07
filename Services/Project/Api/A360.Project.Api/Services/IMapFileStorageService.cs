namespace A360.Project.Api.Services;

public interface IMapFileStorageService
{
    Task<string> SaveMapFileAsync(IFormFile file, string category, CancellationToken cancellationToken = default);
}
