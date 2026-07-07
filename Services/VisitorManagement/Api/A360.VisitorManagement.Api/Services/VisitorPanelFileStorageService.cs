using A360.Media.Client;

namespace A360.VisitorManagement.Api.Services;

public sealed class VisitorPanelFileStorageService : IVisitorPanelFileStorageService
{
    private readonly IMediaStorageClient _mediaStorageClient;

    public VisitorPanelFileStorageService(IMediaStorageClient mediaStorageClient)
    {
        _mediaStorageClient = mediaStorageClient;
    }

    public async Task<string> SaveImageFileAsync(IFormFile file, string category, CancellationToken cancellationToken = default)
    {
        await using var stream = file.OpenReadStream();

        return await _mediaStorageClient.UploadAsync(
            stream,
            file.FileName,
            file.ContentType,
            category,
            cancellationToken);
    }
}
