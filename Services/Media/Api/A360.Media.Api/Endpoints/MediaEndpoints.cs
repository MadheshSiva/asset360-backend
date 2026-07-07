using Microsoft.AspNetCore.Mvc;
using A360.Media.Api.Contracts;
using A360.Media.Api.Services;

namespace A360.Media.Api.Endpoints;

public static class MediaEndpoints
{
    public static RouteGroupBuilder MapMediaEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/media")
            .WithTags("Media");

        group.MapPost("/upload", UploadAsync)
            .WithName("UploadMedia")
            .DisableAntiforgery();

        return group;
    }

    private static async Task<IResult> UploadAsync(
        IFormFile file,
        [FromForm] string category,
        IMediaFileStorageService fileStorageService,
        CancellationToken cancellationToken)
    {
        if (file.Length == 0)
        {
            return Results.BadRequest(new { message = "File is required." });
        }

        if (string.IsNullOrWhiteSpace(category))
        {
            return Results.BadRequest(new { message = "Category is required." });
        }

        try
        {
            var url = await fileStorageService.SaveFileAsync(file, category, cancellationToken);
            return Results.Ok(new MediaUploadResponse(url));
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { message = ex.Message });
        }
    }
}
