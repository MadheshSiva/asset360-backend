namespace P360.VisitorManagement.Api.Services;

public interface IVisitorPanelFileStorageService
{
    Task<string> SaveImageFileAsync(IFormFile file, string category, CancellationToken cancellationToken = default);
}
