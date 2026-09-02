using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class BusinessUnitEndpoints
{
    private const string SequenceName = "business_unit";
    private const string BusinessUnitCodePrefix = "BU";

    public static RouteGroupBuilder MapBusinessUnitEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/business-units").WithTags("BusinessUnits");

        group.MapGet("", GetBusinessUnitsAsync).WithName("GetBusinessUnits");
        group.MapGet("/{id}", GetBusinessUnitByIdAsync).WithName("GetBusinessUnitById");
        group.MapPost("", CreateBusinessUnitAsync).WithName("CreateBusinessUnit");
        group.MapPut("/{id}", UpdateBusinessUnitAsync).WithName("UpdateBusinessUnit");
        group.MapDelete("/{id}", DeleteBusinessUnitAsync).WithName("DeleteBusinessUnit");

        return group;
    }

    private static async Task<IResult> GetBusinessUnitsAsync(
        IBusinessUnitRepository repository,
        CancellationToken cancellationToken)
    {
        var businessUnits = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(businessUnits.Select(BusinessUnitResponse.FromEntity));
    }

    private static async Task<IResult> GetBusinessUnitByIdAsync(
        string id,
        IBusinessUnitRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid business unit id." });
        }

        var businessUnit = await repository.GetByIdAsync(id, cancellationToken);
        return businessUnit is null ? Results.NotFound() : Results.Ok(BusinessUnitResponse.FromEntity(businessUnit));
    }

    private static async Task<IResult> CreateBusinessUnitAsync(
        CreateBusinessUnitRequest request,
        IBusinessUnitRepository repository,
        IAssetRepository assetRepository,
        IOrganizationRepository organizationRepository,
        ISequenceGenerator sequenceGenerator,
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

        var organization = await organizationRepository.GetByOrganizationCodeAsync(request.Organization!, cancellationToken);
        if (organization is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["Organization"] = ["No organization exists with this Organization code"]
            });
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var businessUnitCode = $"{BusinessUnitCodePrefix}{nextSequence:D6}";

        var businessUnit = await repository.CreateAsync(
            request.ToEntity(businessUnitCode, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/business-units/{businessUnit.Id}", BusinessUnitResponse.FromEntity(businessUnit));
    }

    private static async Task<IResult> UpdateBusinessUnitAsync(
        string id,
        UpdateBusinessUnitRequest request,
        IBusinessUnitRepository repository,
        IAssetRepository assetRepository,
        IOrganizationRepository organizationRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid business unit id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var businessUnit = await repository.GetByIdAsync(id, cancellationToken);
        if (businessUnit is null)
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

        var organization = await organizationRepository.GetByOrganizationCodeAsync(request.Organization!, cancellationToken);
        if (organization is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["Organization"] = ["No organization exists with this Organization code"]
            });
        }

        request.ApplyTo(businessUnit, asset.AssetName);

        var updated = await repository.UpdateAsync(id, businessUnit, cancellationToken);
        return updated ? Results.Ok(BusinessUnitResponse.FromEntity(businessUnit)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteBusinessUnitAsync(
        string id,
        IBusinessUnitRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid business unit id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
