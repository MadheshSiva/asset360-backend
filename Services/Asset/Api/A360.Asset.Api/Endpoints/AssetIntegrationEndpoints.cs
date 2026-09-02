using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.Asset.Api.Endpoints;

public static class AssetIntegrationEndpoints
{
    private const string SequenceName = "asset-integration";
    private const string IntegrationIdPrefix = "INT";

    public static RouteGroupBuilder MapAssetIntegrationEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-integrations").WithTags("AssetIntegrations");

        group.MapGet("", GetAssetIntegrationsAsync).WithName("GetAssetIntegrations");
        group.MapGet("/{id}", GetAssetIntegrationByIdAsync).WithName("GetAssetIntegrationById");
        group.MapGet("/by-asset/{assetId}", GetAssetIntegrationsByAssetIdAsync).WithName("GetAssetIntegrationsByAssetId");
        group.MapPost("", CreateAssetIntegrationAsync).WithName("CreateAssetIntegration");
        group.MapPut("/{id}", UpdateAssetIntegrationAsync).WithName("UpdateAssetIntegration");
        group.MapDelete("/{id}", DeleteAssetIntegrationAsync).WithName("DeleteAssetIntegration");

        return group;
    }

    private static async Task<IResult> GetAssetIntegrationsAsync(
        IAssetIntegrationRepository repository,
        CancellationToken cancellationToken)
    {
        var integrations = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(integrations.Select(AssetIntegrationResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetIntegrationByIdAsync(
        string id,
        IAssetIntegrationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset integration id." });
        }

        var integration = await repository.GetByIdAsync(id, cancellationToken);
        return integration is null ? Results.NotFound() : Results.Ok(AssetIntegrationResponse.FromEntity(integration));
    }

    private static async Task<IResult> GetAssetIntegrationsByAssetIdAsync(
        string assetId,
        IAssetIntegrationRepository repository,
        CancellationToken cancellationToken)
    {
        var integrations = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(integrations.Select(AssetIntegrationResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetIntegrationAsync(
        CreateAssetIntegrationRequest request,
        IAssetIntegrationRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var integrationId = $"{IntegrationIdPrefix}{nextSequence:D6}";

        var integration = await repository.CreateAsync(request.ToEntity(integrationId), cancellationToken);
        return Results.Created($"/api/asset-integrations/{integration.Id}", AssetIntegrationResponse.FromEntity(integration));
    }

    private static async Task<IResult> UpdateAssetIntegrationAsync(
        string id,
        UpdateAssetIntegrationRequest request,
        IAssetIntegrationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset integration id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var integration = await repository.GetByIdAsync(id, cancellationToken);
        if (integration is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(integration);

        var updated = await repository.UpdateAsync(id, integration, cancellationToken);
        return updated ? Results.Ok(AssetIntegrationResponse.FromEntity(integration)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetIntegrationAsync(
        string id,
        IAssetIntegrationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset integration id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
