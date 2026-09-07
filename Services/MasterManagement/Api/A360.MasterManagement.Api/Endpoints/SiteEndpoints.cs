using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class SiteEndpoints
{
    private const string SequenceName = "site";
    private const string SiteCodePrefix = "SITE";

    public static RouteGroupBuilder MapSiteEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/sites").WithTags("Sites");

        group.MapGet("", GetSitesAsync).WithName("GetSites");
        group.MapGet("/{id}", GetSiteByIdAsync).WithName("GetSiteById");
        group.MapPost("", CreateSiteAsync).WithName("CreateSite");
        group.MapPut("/{id}", UpdateSiteAsync).WithName("UpdateSite");
        group.MapDelete("/{id}", DeleteSiteAsync).WithName("DeleteSite");

        return group;
    }

    private static async Task<IResult> GetSitesAsync(
        ISiteRepository repository,
        CancellationToken cancellationToken)
    {
        var sites = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(sites.Select(SiteResponse.FromEntity));
    }

    private static async Task<IResult> GetSiteByIdAsync(
        string id,
        ISiteRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid site id." });
        }

        var site = await repository.GetByIdAsync(id, cancellationToken);
        if (site is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Site", site.SiteCode, site.SiteName, EventAction.Viewed, cancellationToken);

        return Results.Ok(SiteResponse.FromEntity(site));
    }

    private static async Task<IResult> CreateSiteAsync(
        CreateSiteRequest request,
        ISiteRepository repository,
        IAssetRepository assetRepository,
        IOrganizationRepository organizationRepository,
        IBusinessUnitRepository businessUnitRepository,
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

        var organization = await organizationRepository.GetByOrganizationCodeAsync(request.Organization!, cancellationToken);
        if (organization is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["Organization"] = ["No organization exists with this Organization code"]
            });
        }

        var businessUnit = await businessUnitRepository.GetByBusinessUnitCodeAsync(request.BusinessUnit!, cancellationToken);
        if (businessUnit is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["BusinessUnit"] = ["No business unit exists with this BusinessUnit code"]
            });
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var siteCode = $"{SiteCodePrefix}{nextSequence:D6}";

        var site = await repository.CreateAsync(
            request.ToEntity(siteCode, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("Site", site.SiteCode, site.SiteName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/sites/{site.Id}", SiteResponse.FromEntity(site));
    }

    private static async Task<IResult> UpdateSiteAsync(
        string id,
        UpdateSiteRequest request,
        ISiteRepository repository,
        IAssetRepository assetRepository,
        IOrganizationRepository organizationRepository,
        IBusinessUnitRepository businessUnitRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid site id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var site = await repository.GetByIdAsync(id, cancellationToken);
        if (site is null)
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

        var businessUnit = await businessUnitRepository.GetByBusinessUnitCodeAsync(request.BusinessUnit!, cancellationToken);
        if (businessUnit is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["BusinessUnit"] = ["No business unit exists with this BusinessUnit code"]
            });
        }

        request.ApplyTo(site, asset.AssetName);

        var updated = await repository.UpdateAsync(id, site, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Site", site.SiteCode, site.SiteName, EventAction.Updated, cancellationToken);

        return Results.Ok(SiteResponse.FromEntity(site));
    }

    private static async Task<IResult> DeleteSiteAsync(
        string id,
        ISiteRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid site id." });
        }

        var site = await repository.GetByIdAsync(id, cancellationToken);
        if (site is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Site", site.SiteCode, site.SiteName, EventAction.Deleted, cancellationToken);

        return Results.NoContent();
    }
}
