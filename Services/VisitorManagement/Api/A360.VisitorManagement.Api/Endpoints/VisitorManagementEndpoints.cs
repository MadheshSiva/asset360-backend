using A360.Repository.Repositories;
using A360.VisitorManagement.Api.Contracts;
using A360.VisitorManagement.Api.Services;
using A360.VisitorManagement.Api.Validation;
using A360.VisitorManagement.Repository.Repositories;
using PanelSettingEntity = A360.VisitorManagement.Domain.Entities.VisitorPanelSetting;

namespace A360.VisitorManagement.Api.Endpoints;

public static class VisitorManagementEndpoints
{
    private const string BackgroundImageCategory = "VisitorBackground";
    private const string LogoImageCategory = "VisitorLogo";

    public static RouteGroupBuilder MapVisitorManagementEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/visitorpanelsettings")
            .WithTags("VisitorPanelSettings");

        group.MapGet("/client/{clientId}", GetByClientIdAsync)
            .WithName("GetVisitorPanelSettingByClientId");

        group.MapPost("", CreateAsync)
            .WithName("CreateVisitorPanelSetting");

        group.MapPut("/{id}", UpdateAsync)
            .WithName("UpdateVisitorPanelSetting");

        group.MapPost("/{id}/background", UploadBackgroundImageAsync)
            .WithName("UploadVisitorPanelBackgroundImage")
            .DisableAntiforgery();

        group.MapPost("/{id}/logo", UploadLogoImageAsync)
            .WithName("UploadVisitorPanelLogoImage")
            .DisableAntiforgery();

        return group;
    }

    private static async Task<IResult> GetByClientIdAsync(
        string clientId,
        IVisitorManagementRepository repository,
        CancellationToken cancellationToken)
    {
        var setting = await repository.GetByClientIdAsync(
            clientId,
            cancellationToken);

        return setting is null
            ? Results.NotFound()
            : Results.Ok(VisitorPanelSettingResponse.FromEntity(setting));
    }

    private static async Task<IResult> CreateAsync(
        CreateVisitorPanelSettingRequest request,
        IVisitorManagementRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var clientId = request.ClientId!.Trim();

        var existing = await repository.GetByClientIdAsync(
            clientId,
            cancellationToken);

        if (existing is not null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["ClientId"] =
                [
                    "A visitor panel setting for this client already exists."
                ]
            });
        }

        var setting = request.ToEntity();

        var created = await repository.CreateAsync(
            setting,
            cancellationToken);

        return Results.Created(
            $"/api/visitorpanelsettings/{created.Id}",
            VisitorPanelSettingResponse.FromEntity(created));
    }

    private static async Task<IResult> UpdateAsync(
        string id,
        UpdateVisitorPanelSettingRequest request,
        IVisitorManagementRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor panel setting id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var setting = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (setting is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(setting);

        var updated = await repository.UpdateAsync(
            id,
            setting,
            cancellationToken);

        return updated
            ? Results.Ok(VisitorPanelSettingResponse.FromEntity(setting))
            : Results.NotFound();
    }

    private static async Task<IResult> UploadBackgroundImageAsync(
        string id,
        IFormFile file,
        IVisitorManagementRepository repository,
        IVisitorPanelFileStorageService fileStorageService,
        CancellationToken cancellationToken)
    {
        return await UploadImageAsync(
            id,
            file,
            BackgroundImageCategory,
            repository,
            fileStorageService,
            (setting, path) => setting.BackgroundImg = path,
            cancellationToken);
    }

    private static async Task<IResult> UploadLogoImageAsync(
        string id,
        IFormFile file,
        IVisitorManagementRepository repository,
        IVisitorPanelFileStorageService fileStorageService,
        CancellationToken cancellationToken)
    {
        return await UploadImageAsync(
            id,
            file,
            LogoImageCategory,
            repository,
            fileStorageService,
            (setting, path) => setting.Logo = path,
            cancellationToken);
    }

    private static async Task<IResult> UploadImageAsync(
        string id,
        IFormFile file,
        string category,
        IVisitorManagementRepository repository,
        IVisitorPanelFileStorageService fileStorageService,
        Action<PanelSettingEntity, string> applyImagePath,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor panel setting id." });
        }

        if (file.Length == 0)
        {
            return Results.BadRequest(
                new { message = "Image file is required." });
        }

        var setting = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (setting is null)
        {
            return Results.NotFound();
        }

        string imagePath;
        try
        {
            imagePath = await fileStorageService.SaveImageFileAsync(
                file,
                category,
                cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { message = ex.Message });
        }

        applyImagePath(setting, imagePath);

        var updated = await repository.UpdateAsync(
            id,
            setting,
            cancellationToken);

        return updated
            ? Results.Ok(VisitorPanelSettingResponse.FromEntity(setting))
            : Results.NotFound();
    }
}
