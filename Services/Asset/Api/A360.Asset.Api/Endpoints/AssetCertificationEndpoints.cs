using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;
using A360.Domain.Entities;
using A360.Repository.Activity;

namespace A360.Asset.Api.Endpoints;

public static class AssetCertificationEndpoints
{
    private const string SequenceName = "asset-certification";
    private const string CertificationIdPrefix = "CRT";

    public static RouteGroupBuilder MapAssetCertificationEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-certifications").WithTags("AssetCertifications");

        group.MapGet("", GetAssetCertificationsAsync).WithName("GetAssetCertifications");
        group.MapGet("/{id}", GetAssetCertificationByIdAsync).WithName("GetAssetCertificationById");
        group.MapGet("/by-asset/{assetId}", GetAssetCertificationsByAssetIdAsync).WithName("GetAssetCertificationsByAssetId");
        group.MapPost("", CreateAssetCertificationAsync).WithName("CreateAssetCertification");
        group.MapPut("/{id}", UpdateAssetCertificationAsync).WithName("UpdateAssetCertification");
        group.MapDelete("/{id}", DeleteAssetCertificationAsync).WithName("DeleteAssetCertification");

        return group;
    }

    private static async Task<IResult> GetAssetCertificationsAsync(
        IAssetCertificationRepository repository,
        CancellationToken cancellationToken)
    {
        var certifications = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(certifications.Select(AssetCertificationResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetCertificationByIdAsync(
        string id,
        IAssetCertificationRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset certification id." });
        }

        var certification = await repository.GetByIdAsync(id, cancellationToken);
        if (certification is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetCertification", certification.CertificationId, certification.AssetName, EventAction.Viewed, cancellationToken);
        return Results.Ok(AssetCertificationResponse.FromEntity(certification));
    }

    private static async Task<IResult> GetAssetCertificationsByAssetIdAsync(
        string assetId,
        IAssetCertificationRepository repository,
        CancellationToken cancellationToken)
    {
        var certifications = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(certifications.Select(AssetCertificationResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetCertificationAsync(
        CreateAssetCertificationRequest request,
        IAssetCertificationRepository repository,
        ISequenceGenerator sequenceGenerator,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var certificationId = $"{CertificationIdPrefix}{nextSequence:D6}";

        var certification = await repository.CreateAsync(request.ToEntity(certificationId), cancellationToken);
        await eventLogger.LogAsync("AssetCertification", certification.CertificationId, certification.AssetName, EventAction.Created, cancellationToken);
        return Results.Created($"/api/asset-certifications/{certification.Id}", AssetCertificationResponse.FromEntity(certification));
    }

    private static async Task<IResult> UpdateAssetCertificationAsync(
        string id,
        UpdateAssetCertificationRequest request,
        IAssetCertificationRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset certification id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var certification = await repository.GetByIdAsync(id, cancellationToken);
        if (certification is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(certification);

        var updated = await repository.UpdateAsync(id, certification, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetCertification", certification.CertificationId, certification.AssetName, EventAction.Updated, cancellationToken);
        return Results.Ok(AssetCertificationResponse.FromEntity(certification));
    }

    private static async Task<IResult> DeleteAssetCertificationAsync(
        string id,
        IAssetCertificationRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset certification id." });
        }

        var certification = await repository.GetByIdAsync(id, cancellationToken);
        if (certification is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetCertification", certification.CertificationId, certification.AssetName, EventAction.Deleted, cancellationToken);
        return Results.NoContent();
    }
}
