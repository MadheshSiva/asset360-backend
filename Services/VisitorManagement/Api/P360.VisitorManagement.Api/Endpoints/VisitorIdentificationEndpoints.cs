using P360.Repository.Repositories;
using P360.VisitorManagement.Api.Contracts;
using P360.VisitorManagement.Api.Validation;
using P360.VisitorManagement.Repository.Repositories;

namespace P360.VisitorManagement.Api.Endpoints;

public static class VisitorIdentificationEndpoints
{
    public static RouteGroupBuilder MapVisitorIdentificationEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/visitoridentifications")
            .WithTags("VisitorIdentifications");

        group.MapGet("", GetAllAsync)
            .WithName("GetAllVisitorIdentifications");

        group.MapGet("/{id}", GetByIdAsync)
            .WithName("GetVisitorIdentificationById");

        group.MapGet("/type/{identificationType}", GetByIdentificationTypeAsync)
            .WithName("GetVisitorIdentificationsByIdentificationType");

        group.MapPost("", CreateAsync)
            .WithName("CreateVisitorIdentification");

        group.MapPut("/{id}", UpdateAsync)
            .WithName("UpdateVisitorIdentification");

        group.MapDelete("/{id}", DeleteAsync)
            .WithName("DeleteVisitorIdentification");

        return group;
    }

    private static async Task<IResult> GetAllAsync(
        IVisitorIdentificationRepository repository,
        CancellationToken cancellationToken)
    {
        var identifications = await repository.GetAllAsync(cancellationToken);

        return Results.Ok(
            identifications.Select(VisitorIdentificationResponse.FromEntity));
    }

    private static async Task<IResult> GetByIdAsync(
        string id,
        IVisitorIdentificationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor identification id." });
        }

        var identification = await repository.GetByIdAsync(id, cancellationToken);

        return identification is null
            ? Results.NotFound()
            : Results.Ok(VisitorIdentificationResponse.FromEntity(identification));
    }

    private static async Task<IResult> GetByIdentificationTypeAsync(
        string identificationType,
        IVisitorIdentificationRepository repository,
        CancellationToken cancellationToken)
    {
        var identifications = await repository.GetByIdentificationTypeAsync(
            identificationType,
            cancellationToken);

        return Results.Ok(
            identifications.Select(VisitorIdentificationResponse.FromEntity));
    }

    private static async Task<IResult> CreateAsync(
        CreateVisitorIdentificationRequest request,
        IVisitorIdentificationRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var identification = request.ToEntity();

        var created = await repository.CreateAsync(identification, cancellationToken);

        return Results.Created(
            $"/api/visitoridentifications/{created.Id}",
            VisitorIdentificationResponse.FromEntity(created));
    }

    private static async Task<IResult> UpdateAsync(
        string id,
        UpdateVisitorIdentificationRequest request,
        IVisitorIdentificationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor identification id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var identification = await repository.GetByIdAsync(id, cancellationToken);

        if (identification is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(identification);

        var updated = await repository.UpdateAsync(id, identification, cancellationToken);

        return updated
            ? Results.Ok(VisitorIdentificationResponse.FromEntity(identification))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteAsync(
        string id,
        IVisitorIdentificationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor identification id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}
