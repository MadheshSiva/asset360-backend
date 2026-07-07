using P360.OTManagement.Api.Contracts;
using P360.OTManagement.Api.Validation;
using P360.OTManagement.Repository.Repositories;
using P360.Repository.Repositories;

namespace P360.OTManagement.Api.Endpoints;

public static class OTSchedulingEndpoints
{
    public static RouteGroupBuilder MapOTSchedulingEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/otscheduling")
            .WithTags("OTScheduling");

        group.MapGet("", GetOTSchedulingsAsync)
            .WithName("GetOTSchedulings");

        group.MapGet("/{id}", GetOTSchedulingByIdAsync)
            .WithName("GetOTSchedulingById");

        group.MapPost("", CreateOTSchedulingAsync)
            .WithName("CreateOTScheduling");

        group.MapPut("/{id}", UpdateOTSchedulingAsync)
            .WithName("UpdateOTScheduling");

        group.MapDelete("/{id}", DeleteOTSchedulingAsync)
            .WithName("DeleteOTScheduling");

        return group;
    }

    private static async Task<IResult> GetOTSchedulingsAsync(
        IOTSchedulingRepository repository,
        CancellationToken cancellationToken)
    {
        var otSchedulings = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            otSchedulings.Select(
                OTSchedulingResponse.FromEntity));
    }

    private static async Task<IResult> GetOTSchedulingByIdAsync(
        string id,
        IOTSchedulingRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid OT Scheduling id." });
        }

        var otScheduling = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return otScheduling is null
            ? Results.NotFound()
            : Results.Ok(
                OTSchedulingResponse.FromEntity(
                    otScheduling));
    }

    private static async Task<IResult> CreateOTSchedulingAsync(
        CreateOTSchedulingRequest request,
        IOTSchedulingRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(
                validationErrors);
        }

        var otScheduling = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/otscheduling/{otScheduling.Id}",
            OTSchedulingResponse.FromEntity(
                otScheduling));
    }

    private static async Task<IResult> UpdateOTSchedulingAsync(
        string id,
        UpdateOTSchedulingRequest request,
        IOTSchedulingRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid OT Scheduling id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(
                validationErrors);
        }

        var otScheduling = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (otScheduling is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(otScheduling);

        var updated = await repository.UpdateAsync(
            id,
            otScheduling,
            cancellationToken);

        return updated
            ? Results.Ok(
                OTSchedulingResponse.FromEntity(
                    otScheduling))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteOTSchedulingAsync(
        string id,
        IOTSchedulingRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid OT Scheduling id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}