using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class AuditorDetailEndpoints
{
    private const string SequenceName = "auditor_detail";
    private const string AuditorIdPrefix = "AUD";

    public static RouteGroupBuilder MapAuditorDetailEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/auditor-details").WithTags("AuditorDetails");

        group.MapGet("", GetAuditorDetailsAsync).WithName("GetAuditorDetails");
        group.MapGet("/{id}", GetAuditorDetailByIdAsync).WithName("GetAuditorDetailById");
        group.MapPost("", CreateAuditorDetailAsync).WithName("CreateAuditorDetail");
        group.MapPut("/{id}", UpdateAuditorDetailAsync).WithName("UpdateAuditorDetail");
        group.MapDelete("/{id}", DeleteAuditorDetailAsync).WithName("DeleteAuditorDetail");

        return group;
    }

    private static async Task<IResult> GetAuditorDetailsAsync(
        IAuditorDetailRepository repository,
        CancellationToken cancellationToken)
    {
        var auditorDetails = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(auditorDetails.Select(AuditorDetailResponse.FromEntity));
    }

    private static async Task<IResult> GetAuditorDetailByIdAsync(
        string id,
        IAuditorDetailRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid auditor detail id." });
        }

        var auditorDetail = await repository.GetByIdAsync(id, cancellationToken);
        return auditorDetail is null ? Results.NotFound() : Results.Ok(AuditorDetailResponse.FromEntity(auditorDetail));
    }

    private static async Task<IResult> CreateAuditorDetailAsync(
        CreateAuditorDetailRequest request,
        IAuditorDetailRepository repository,
        IAssetRepository assetRepository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var asset = await assetRepository.GetByAssetIdAsync(request.AssetId!, cancellationToken);
        if (asset is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["AssetId"] = ["No asset exists with this AssetId"]
            });
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var auditorId = $"{AuditorIdPrefix}{nextSequence:D6}";

        var auditorDetail = await repository.CreateAsync(
            request.ToEntity(auditorId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/auditor-details/{auditorDetail.Id}", AuditorDetailResponse.FromEntity(auditorDetail));
    }

    private static async Task<IResult> UpdateAuditorDetailAsync(
        string id,
        UpdateAuditorDetailRequest request,
        IAuditorDetailRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid auditor detail id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var auditorDetail = await repository.GetByIdAsync(id, cancellationToken);
        if (auditorDetail is null)
        {
            return Results.NotFound();
        }

        var asset = await assetRepository.GetByAssetIdAsync(request.AssetId!, cancellationToken);
        if (asset is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["AssetId"] = ["No asset exists with this AssetId"]
            });
        }

        request.ApplyTo(auditorDetail, asset.AssetName);

        var updated = await repository.UpdateAsync(id, auditorDetail, cancellationToken);
        return updated ? Results.Ok(AuditorDetailResponse.FromEntity(auditorDetail)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAuditorDetailAsync(
        string id,
        IAuditorDetailRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid auditor detail id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
