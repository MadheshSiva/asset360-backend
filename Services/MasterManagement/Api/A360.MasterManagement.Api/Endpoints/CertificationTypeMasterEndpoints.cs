using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class CertificationTypeMasterEndpoints
{
    private const string SequenceName = "certification_type_master";
    private const string CertificationIdPrefix = "CTM";

    public static RouteGroupBuilder MapCertificationTypeMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/certification-type-masters").WithTags("CertificationTypeMasters");

        group.MapGet("", GetCertificationTypeMastersAsync).WithName("GetCertificationTypeMasters");
        group.MapGet("/{id}", GetCertificationTypeMasterByIdAsync).WithName("GetCertificationTypeMasterById");
        group.MapPost("", CreateCertificationTypeMasterAsync).WithName("CreateCertificationTypeMaster");
        group.MapPut("/{id}", UpdateCertificationTypeMasterAsync).WithName("UpdateCertificationTypeMaster");
        group.MapDelete("/{id}", DeleteCertificationTypeMasterAsync).WithName("DeleteCertificationTypeMaster");

        return group;
    }

    private static async Task<IResult> GetCertificationTypeMastersAsync(
        ICertificationTypeMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var certificationTypeMasters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(certificationTypeMasters.Select(CertificationTypeMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetCertificationTypeMasterByIdAsync(
        string id,
        ICertificationTypeMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid certification type master id." });
        }

        var certificationTypeMaster = await repository.GetByIdAsync(id, cancellationToken);
        return certificationTypeMaster is null ? Results.NotFound() : Results.Ok(CertificationTypeMasterResponse.FromEntity(certificationTypeMaster));
    }

    private static async Task<IResult> CreateCertificationTypeMasterAsync(
        CreateCertificationTypeMasterRequest request,
        ICertificationTypeMasterRepository repository,
        IAssetRepository assetRepository,
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

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var certificationId = $"{CertificationIdPrefix}{nextSequence:D6}";

        var certificationTypeMaster = await repository.CreateAsync(
            request.ToEntity(certificationId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/certification-type-masters/{certificationTypeMaster.Id}", CertificationTypeMasterResponse.FromEntity(certificationTypeMaster));
    }

    private static async Task<IResult> UpdateCertificationTypeMasterAsync(
        string id,
        UpdateCertificationTypeMasterRequest request,
        ICertificationTypeMasterRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid certification type master id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var certificationTypeMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (certificationTypeMaster is null)
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

        request.ApplyTo(certificationTypeMaster, asset.AssetName);

        var updated = await repository.UpdateAsync(id, certificationTypeMaster, cancellationToken);
        return updated ? Results.Ok(CertificationTypeMasterResponse.FromEntity(certificationTypeMaster)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteCertificationTypeMasterAsync(
        string id,
        ICertificationTypeMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid certification type master id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
