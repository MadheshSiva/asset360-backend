using P360.Media.Client;

namespace P360.Project.Api.Services;

public sealed class MapFileStorageService : IMapFileStorageService
{
    private readonly IMediaStorageClient _mediaStorageClient;

    public MapFileStorageService(IMediaStorageClient mediaStorageClient)
    {
        _mediaStorageClient = mediaStorageClient;
    }

    public async Task<string> SaveMapFileAsync(IFormFile file, string category, CancellationToken cancellationToken = default)
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
