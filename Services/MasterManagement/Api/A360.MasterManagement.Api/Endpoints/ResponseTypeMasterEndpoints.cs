using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class ResponseTypeMasterEndpoints
{
    private const string SequenceName = "response_type_master";
    private const string TypeIdPrefix = "RTM";

    public static RouteGroupBuilder MapResponseTypeMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/response-type-masters").WithTags("ResponseTypeMasters");

        group.MapGet("", GetResponseTypeMastersAsync).WithName("GetResponseTypeMasters");
        group.MapGet("/{id}", GetResponseTypeMasterByIdAsync).WithName("GetResponseTypeMasterById");
        group.MapPost("", CreateResponseTypeMasterAsync).WithName("CreateResponseTypeMaster");
        group.MapPut("/{id}", UpdateResponseTypeMasterAsync).WithName("UpdateResponseTypeMaster");
        group.MapDelete("/{id}", DeleteResponseTypeMasterAsync).WithName("DeleteResponseTypeMaster");

        return group;
    }

    private static async Task<IResult> GetResponseTypeMastersAsync(
        IResponseTypeMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var responseTypeMasters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(responseTypeMasters.Select(ResponseTypeMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetResponseTypeMasterByIdAsync(
        string id,
        IResponseTypeMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid response type master id." });
        }

        var responseTypeMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (responseTypeMaster is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("ResponseTypeMaster", responseTypeMaster.TypeId, responseTypeMaster.TypeName, EventAction.Viewed, cancellationToken);

        return Results.Ok(ResponseTypeMasterResponse.FromEntity(responseTypeMaster));
    }

    private static async Task<IResult> CreateResponseTypeMasterAsync(
        CreateResponseTypeMasterRequest request,
        IResponseTypeMasterRepository repository,
        IAssetRepository assetRepository,
        ISequenceGenerator sequenceGenerator,
        IEventLogger eventLogger,
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
        var typeId = $"{TypeIdPrefix}{nextSequence:D6}";

        var responseTypeMaster = await repository.CreateAsync(
            request.ToEntity(typeId, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("ResponseTypeMaster", responseTypeMaster.TypeId, responseTypeMaster.TypeName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/response-type-masters/{responseTypeMaster.Id}", ResponseTypeMasterResponse.FromEntity(responseTypeMaster));
    }

    private static async Task<IResult> UpdateResponseTypeMasterAsync(
        string id,
        UpdateResponseTypeMasterRequest request,
        IResponseTypeMasterRepository repository,
        IAssetRepository assetRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid response type master id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var responseTypeMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (responseTypeMaster is null)
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

        request.ApplyTo(responseTypeMaster, asset.AssetName);

        var updated = await repository.UpdateAsync(id, responseTypeMaster, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("ResponseTypeMaster", responseTypeMaster.TypeId, responseTypeMaster.TypeName, EventAction.Updated, cancellationToken);

        return Results.Ok(ResponseTypeMasterResponse.FromEntity(responseTypeMaster));
    }

    private static async Task<IResult> DeleteResponseTypeMasterAsync(
        string id,
        IResponseTypeMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid response type master id." });
        }

        var responseTypeMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (responseTypeMaster is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("ResponseTypeMaster", responseTypeMaster.TypeId, responseTypeMaster.TypeName, EventAction.Deleted, cancellationToken);

        return Results.NoContent();
    }
}
