using P360.Repository.Repositories;
using P360.VisitorManagement.Api.Contracts;
using P360.VisitorManagement.Api.Validation;
using P360.VisitorManagement.Repository.Repositories;

namespace P360.VisitorManagement.Api.Endpoints;

public static class VisitorApprovalEndpoints
{
    public static RouteGroupBuilder MapVisitorApprovalEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/visitorapprovals")
            .WithTags("VisitorApprovals");

        group.MapGet("", GetAllAsync)
            .WithName("GetVisitorApprovals");

        group.MapGet("/{id}", GetByIdAsync)
            .WithName("GetVisitorApprovalById");

        group.MapPost("", CreateAsync)
            .WithName("CreateVisitorApproval");

        group.MapPut("/{id}", UpdateAsync)
            .WithName("UpdateVisitorApproval");

        group.MapDelete("/{id}", DeleteAsync)
            .WithName("DeleteVisitorApproval");

        return group;
    }

    private static async Task<IResult> GetAllAsync(
        IVisitorApprovalRepository repository,
        CancellationToken cancellationToken)
    {
        var approvals = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            approvals.Select(VisitorApprovalResponse.FromEntity));
    }

    private static async Task<IResult> GetByIdAsync(
        string id,
        IVisitorApprovalRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor approval id." });
        }

        var approval = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return approval is null
            ? Results.NotFound()
            : Results.Ok(VisitorApprovalResponse.FromEntity(approval));
    }

    private static async Task<IResult> CreateAsync(
        CreateVisitorApprovalRequest request,
        IVisitorApprovalRepository repository,
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
            $"/api/visitorapprovals/{created.Id}",
            VisitorApprovalResponse.FromEntity(created));
    }

    private static async Task<IResult> UpdateAsync(
        string id,
        UpdateVisitorApprovalRequest request,
        IVisitorApprovalRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor approval id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var approval = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (approval is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(approval);

        var updated = await repository.UpdateAsync(
            id,
            approval,
            cancellationToken);

        return updated
            ? Results.Ok(VisitorApprovalResponse.FromEntity(approval))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteAsync(
        string id,
        IVisitorApprovalRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor approval id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}
