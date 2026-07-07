using P360.People.Api.Contracts;
using P360.People.Api.Validation;
using P360.People.Repository.Repositories;
using P360.Repository.Repositories;

namespace P360.People.Api.Endpoints;

public static class ContractorEndpoints
{
    public static RouteGroupBuilder MapContractorEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/contractors")
            .WithTags("Contractors");

        group.MapGet("", GetContractorsAsync)
            .WithName("GetContractors");

        group.MapGet("/{id}", GetContractorByIdAsync)
            .WithName("GetContractorById");

        group.MapPost("", CreateContractorAsync)
            .WithName("CreateContractor");

        group.MapPut("/{id}", UpdateContractorAsync)
            .WithName("UpdateContractor");

        group.MapDelete("/{id}", DeleteContractorAsync)
            .WithName("DeleteContractor");

        return group;
    }

    private static async Task<IResult> GetContractorsAsync(
        IContractorRepository repository,
        CancellationToken cancellationToken)
    {
        var contractors = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            contractors.Select(ContractorResponse.FromEntity));
    }

    private static async Task<IResult> GetContractorByIdAsync(
        string id,
        IContractorRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid contractor id." });
        }

        var contractor = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return contractor is null
            ? Results.NotFound()
            : Results.Ok(ContractorResponse.FromEntity(contractor));
    }

    private static async Task<IResult> CreateContractorAsync(
        CreateContractorRequest request,
        IContractorRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var contractor = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/contractors/{contractor.Id}",
            ContractorResponse.FromEntity(contractor));
    }

    private static async Task<IResult> UpdateContractorAsync(
        string id,
        UpdateContractorRequest request,
        IContractorRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid contractor id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var contractor = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (contractor is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(contractor);

        var updated = await repository.UpdateAsync(
            id,
            contractor,
            cancellationToken);

        return updated
            ? Results.Ok(ContractorResponse.FromEntity(contractor))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteContractorAsync(
        string id,
        IContractorRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid contractor id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}