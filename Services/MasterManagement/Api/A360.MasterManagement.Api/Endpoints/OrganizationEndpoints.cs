using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class OrganizationEndpoints
{
    private const string SequenceName = "organization";
    private const string OrganizationCodePrefix = "ORG";

    public static RouteGroupBuilder MapOrganizationEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/organizations").WithTags("Organizations");

        group.MapGet("", GetOrganizationsAsync).WithName("GetOrganizations");
        group.MapGet("/{id}", GetOrganizationByIdAsync).WithName("GetOrganizationById");
        group.MapPost("", CreateOrganizationAsync).WithName("CreateOrganization");
        group.MapPut("/{id}", UpdateOrganizationAsync).WithName("UpdateOrganization");
        group.MapDelete("/{id}", DeleteOrganizationAsync).WithName("DeleteOrganization");

        return group;
    }

    private static async Task<IResult> GetOrganizationsAsync(
        IOrganizationRepository repository,
        CancellationToken cancellationToken)
    {
        var organizations = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(organizations.Select(OrganizationResponse.FromEntity));
    }

    private static async Task<IResult> GetOrganizationByIdAsync(
        string id,
        IOrganizationRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid organization id." });
        }

        var organization = await repository.GetByIdAsync(id, cancellationToken);
        if (organization is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Organization", organization.OrganizationCode, organization.OrganizationName, EventAction.Viewed, cancellationToken);
        return Results.Ok(OrganizationResponse.FromEntity(organization));
    }

    private static async Task<IResult> CreateOrganizationAsync(
        CreateOrganizationRequest request,
        IOrganizationRepository repository,
        IAssetRepository assetRepository,
        ISequenceGenerator sequenceGenerator,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var asset = await assetRepository.GetByAssetIdAsync(request.AssetId!, cancellationToken);
        if (asset is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["AssetId"] = ["No asset exists with this AssetId"]
            });
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var organizationCode = $"{OrganizationCodePrefix}{nextSequence:D6}";

        var organization = await repository.CreateAsync(
            request.ToEntity(organizationCode, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("Organization", organization.OrganizationCode, organization.OrganizationName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/organizations/{organization.Id}", OrganizationResponse.FromEntity(organization));
    }

    private static async Task<IResult> UpdateOrganizationAsync(
        string id,
        UpdateOrganizationRequest request,
        IOrganizationRepository repository,
        IAssetRepository assetRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid organization id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var organization = await repository.GetByIdAsync(id, cancellationToken);
        if (organization is null)
        {
            return Results.NotFound();
        }

        var asset = await assetRepository.GetByAssetIdAsync(request.AssetId!, cancellationToken);
        if (asset is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["AssetId"] = ["No asset exists with this AssetId"]
            });
        }

        request.ApplyTo(organization, asset.AssetName);

        var updated = await repository.UpdateAsync(id, organization, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Organization", organization.OrganizationCode, organization.OrganizationName, EventAction.Updated, cancellationToken);
        return Results.Ok(OrganizationResponse.FromEntity(organization));
    }

    private static async Task<IResult> DeleteOrganizationAsync(
        string id,
        IOrganizationRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid organization id." });
        }

        var organization = await repository.GetByIdAsync(id, cancellationToken);
        if (organization is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Organization", organization.OrganizationCode, organization.OrganizationName, EventAction.Deleted, cancellationToken);
        return Results.NoContent();
    }
}
