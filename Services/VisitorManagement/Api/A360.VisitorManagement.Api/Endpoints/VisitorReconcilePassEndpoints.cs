using A360.Repository.Repositories;
using A360.VisitorManagement.Api.Contracts;
using A360.VisitorManagement.Api.Validation;
using A360.VisitorManagement.Repository.Repositories;

namespace A360.VisitorManagement.Api.Endpoints;

public static class VisitorReconcilePassEndpoints
{
    public static RouteGroupBuilder MapVisitorReconcilePassEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/visitorreconcilepasses")
            .WithTags("VisitorReconcilePasses");

        group.MapGet("", GetAllAsync)
            .WithName("GetVisitorReconcilePasses");

        group.MapGet("/{id}", GetByIdAsync)
            .WithName("GetVisitorReconcilePassById");

        group.MapPost("", CreateAsync)
            .WithName("CreateVisitorReconcilePass");

        group.MapPut("/{id}", UpdateAsync)
            .WithName("UpdateVisitorReconcilePass");

        group.MapDelete("/{id}", DeleteAsync)
            .WithName("DeleteVisitorReconcilePass");

        return group;
    }

    private static async Task<IResult> GetAllAsync(
        IVisitorReconcilePassRepository repository,
        CancellationToken cancellationToken)
    {
        var reconcilePasses = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            reconcilePasses.Select(VisitorReconcilePassResponse.FromEntity));
    }

    private static async Task<IResult> GetByIdAsync(
        string id,
        IVisitorReconcilePassRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid reconcile pass id." });
        }

        var reconcilePass = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return reconcilePass is null
            ? Results.NotFound()
            : Results.Ok(VisitorReconcilePassResponse.FromEntity(reconcilePass));
    }

    private static async Task<IResult> CreateAsync(
        CreateVisitorReconcilePassRequest request,
        IVisitorReconcilePassRepository repository,
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
            $"/api/visitorreconcilepasses/{created.Id}",
            VisitorReconcilePassResponse.FromEntity(created));
    }

    private static async Task<IResult> UpdateAsync(
        string id,
        UpdateVisitorReconcilePassRequest request,
        IVisitorReconcilePassRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid reconcile pass id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var reconcilePass = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (reconcilePass is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(reconcilePass);

        var updated = await repository.UpdateAsync(
            id,
            reconcilePass,
            cancellationToken);

        return updated
            ? Results.Ok(VisitorReconcilePassResponse.FromEntity(reconcilePass))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteAsync(
        string id,
        IVisitorReconcilePassRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid reconcile pass id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}
