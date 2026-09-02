using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.Asset.Api.Endpoints;

public static class AssetIncidentEndpoints
{
    private const string SequenceName = "asset-incident";
    private const string IncidentIdPrefix = "INC";

    public static RouteGroupBuilder MapAssetIncidentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-incidents").WithTags("AssetIncidents");

        group.MapGet("", GetAssetIncidentsAsync).WithName("GetAssetIncidents");
        group.MapGet("/{id}", GetAssetIncidentByIdAsync).WithName("GetAssetIncidentById");
        group.MapGet("/by-asset/{assetId}", GetAssetIncidentsByAssetIdAsync).WithName("GetAssetIncidentsByAssetId");
        group.MapPost("", CreateAssetIncidentAsync).WithName("CreateAssetIncident");
        group.MapPut("/{id}", UpdateAssetIncidentAsync).WithName("UpdateAssetIncident");
        group.MapDelete("/{id}", DeleteAssetIncidentAsync).WithName("DeleteAssetIncident");

        return group;
    }

    private static async Task<IResult> GetAssetIncidentsAsync(
        IAssetIncidentRepository repository,
        CancellationToken cancellationToken)
    {
        var incidents = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(incidents.Select(AssetIncidentResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetIncidentByIdAsync(
        string id,
        IAssetIncidentRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset incident id." });
        }

        var incident = await repository.GetByIdAsync(id, cancellationToken);
        return incident is null ? Results.NotFound() : Results.Ok(AssetIncidentResponse.FromEntity(incident));
    }

    private static async Task<IResult> GetAssetIncidentsByAssetIdAsync(
        string assetId,
        IAssetIncidentRepository repository,
        CancellationToken cancellationToken)
    {
        var incidents = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(incidents.Select(AssetIncidentResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetIncidentAsync(
        CreateAssetIncidentRequest request,
        IAssetIncidentRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var incidentId = $"{IncidentIdPrefix}{nextSequence:D6}";

        var incident = await repository.CreateAsync(request.ToEntity(incidentId), cancellationToken);
        return Results.Created($"/api/asset-incidents/{incident.Id}", AssetIncidentResponse.FromEntity(incident));
    }

    private static async Task<IResult> UpdateAssetIncidentAsync(
        string id,
        UpdateAssetIncidentRequest request,
        IAssetIncidentRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset incident id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var incident = await repository.GetByIdAsync(id, cancellationToken);
        if (incident is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(incident);

        var updated = await repository.UpdateAsync(id, incident, cancellationToken);
        return updated ? Results.Ok(AssetIncidentResponse.FromEntity(incident)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetIncidentAsync(
        string id,
        IAssetIncidentRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset incident id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
