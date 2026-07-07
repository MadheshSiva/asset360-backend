using A360.OTManagement.Api.Contracts;
using A360.OTManagement.Api.Validation;
using A360.OTManagement.Repository.Repositories;
using A360.Repository.Repositories;

namespace A360.OTManagement.Api.Endpoints;

public static class OTManagementEndpoints
{
    public static RouteGroupBuilder MapOTManagementEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/otmanagement")
            .WithTags("OTManagement");

        group.MapGet("", GetOTManagementsAsync)
            .WithName("GetOTManagements");

        group.MapGet("/{id}", GetOTManagementByIdAsync)
            .WithName("GetOTManagementById");

        group.MapPost("", CreateOTManagementAsync)
            .WithName("CreateOTManagement");

        group.MapPut("/{id}", UpdateOTManagementAsync)
            .WithName("UpdateOTManagement");

        group.MapDelete("/{id}", DeleteOTManagementAsync)
            .WithName("DeleteOTManagement");

        return group;
    }

    private static async Task<IResult> GetOTManagementsAsync(
        IOTManagementRepository repository,
        CancellationToken cancellationToken)
    {
        var otManagements = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            otManagements.Select(
                OTManagementResponse.FromEntity));
    }

    private static async Task<IResult> GetOTManagementByIdAsync(
        string id,
        IOTManagementRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid OT Management id." });
        }

        var otManagement = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return otManagement is null
            ? Results.NotFound()
            : Results.Ok(
                OTManagementResponse.FromEntity(
                    otManagement));
    }

    private static async Task<IResult> CreateOTManagementAsync(
        CreateOTManagementRequest request,
        IOTManagementRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(
                validationErrors);
        }

        var otManagement = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/otmanagement/{otManagement.Id}",
            OTManagementResponse.FromEntity(
                otManagement));
    }

    private static async Task<IResult> UpdateOTManagementAsync(
        string id,
        UpdateOTManagementRequest request,
        IOTManagementRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid OT Management id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(
                validationErrors);
        }

        var otManagement = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (otManagement is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(otManagement);

        var updated = await repository.UpdateAsync(
            id,
            otManagement,
            cancellationToken);

        return updated
            ? Results.Ok(
                OTManagementResponse.FromEntity(
                    otManagement))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteOTManagementAsync(
        string id,
        IOTManagementRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid OT Management id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}