using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class CurrentLocationEndpoints
{
    private const string SequenceName = "current_location";
    private const string LocationIdPrefix = "LOC";

    public static RouteGroupBuilder MapCurrentLocationEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/current-locations").WithTags("CurrentLocations");

        group.MapGet("", GetCurrentLocationsAsync).WithName("GetCurrentLocations");
        group.MapGet("/{id}", GetCurrentLocationByIdAsync).WithName("GetCurrentLocationById");
        group.MapPost("", CreateCurrentLocationAsync).WithName("CreateCurrentLocation");
        group.MapPut("/{id}", UpdateCurrentLocationAsync).WithName("UpdateCurrentLocation");
        group.MapDelete("/{id}", DeleteCurrentLocationAsync).WithName("DeleteCurrentLocation");

        return group;
    }

    private static async Task<IResult> GetCurrentLocationsAsync(
        ICurrentLocationRepository repository,
        CancellationToken cancellationToken)
    {
        var currentLocations = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(currentLocations.Select(CurrentLocationResponse.FromEntity));
    }

    private static async Task<IResult> GetCurrentLocationByIdAsync(
        string id,
        ICurrentLocationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid current location id." });
        }

        var currentLocation = await repository.GetByIdAsync(id, cancellationToken);
        return currentLocation is null ? Results.NotFound() : Results.Ok(CurrentLocationResponse.FromEntity(currentLocation));
    }

    private static async Task<IResult> CreateCurrentLocationAsync(
        CreateCurrentLocationRequest request,
        ICurrentLocationRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var locationId = $"{LocationIdPrefix}{nextSequence:D6}";

        var currentLocation = await repository.CreateAsync(
            request.ToEntity(locationId),
            cancellationToken);

        return Results.Created($"/api/current-locations/{currentLocation.Id}", CurrentLocationResponse.FromEntity(currentLocation));
    }

    private static async Task<IResult> UpdateCurrentLocationAsync(
        string id,
        UpdateCurrentLocationRequest request,
        ICurrentLocationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid current location id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var currentLocation = await repository.GetByIdAsync(id, cancellationToken);
        if (currentLocation is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(currentLocation);

        var updated = await repository.UpdateAsync(id, currentLocation, cancellationToken);
        return updated ? Results.Ok(CurrentLocationResponse.FromEntity(currentLocation)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteCurrentLocationAsync(
        string id,
        ICurrentLocationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid current location id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
