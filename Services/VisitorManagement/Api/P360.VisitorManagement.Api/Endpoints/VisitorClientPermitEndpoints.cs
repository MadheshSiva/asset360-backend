using P360.Repository.Repositories;
using P360.VisitorManagement.Api.Contracts;
using P360.VisitorManagement.Api.Validation;
using P360.VisitorManagement.Repository.Repositories;

namespace P360.VisitorManagement.Api.Endpoints;

public static class VisitorClientPermitEndpoints
{
    public static RouteGroupBuilder MapVisitorClientPermitEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/visitorclientpermits")
            .WithTags("VisitorClientPermits");

        group.MapGet("", GetAllAsync)
            .WithName("GetVisitorClientPermits");

        group.MapGet("/{id}", GetByIdAsync)
            .WithName("GetVisitorClientPermitById");

        group.MapPost("", CreateAsync)
            .WithName("CreateVisitorClientPermit");

        group.MapPut("/{id}", UpdateAsync)
            .WithName("UpdateVisitorClientPermit");

        group.MapDelete("/{id}", DeleteAsync)
            .WithName("DeleteVisitorClientPermit");

        return group;
    }

    private static async Task<IResult> GetAllAsync(
        IVisitorClientPermitRepository repository,
        CancellationToken cancellationToken)
    {
        var clientPermits = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            clientPermits.Select(VisitorClientPermitResponse.FromEntity));
    }

    private static async Task<IResult> GetByIdAsync(
        string id,
        IVisitorClientPermitRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid client permit id." });
        }

        var clientPermit = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return clientPermit is null
            ? Results.NotFound()
            : Results.Ok(VisitorClientPermitResponse.FromEntity(clientPermit));
    }

    private static async Task<IResult> CreateAsync(
        CreateVisitorClientPermitRequest request,
        IVisitorClientPermitRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var created = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/visitorclientpermits/{created.Id}",
            VisitorClientPermitResponse.FromEntity(created));
    }

    private static async Task<IResult> UpdateAsync(
        string id,
        UpdateVisitorClientPermitRequest request,
        IVisitorClientPermitRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid client permit id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var clientPermit = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (clientPermit is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(clientPermit);

        var updated = await repository.UpdateAsync(
            id,
            clientPermit,
            cancellationToken);

        return updated
            ? Results.Ok(VisitorClientPermitResponse.FromEntity(clientPermit))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteAsync(
        string id,
        IVisitorClientPermitRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid client permit id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}
