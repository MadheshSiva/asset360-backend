using AssetLifecycleEntity = A360.Asset.Domain.Entities.AssetLifecycle;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetLifecycleRequest(
    string? AssetId,
    string? AssetName,
    DateTime? ProcurementDate,
    DateTime? DeploymentDate,
    string? Status,
    string? DisposalDetails,
    string? ReasonForRetirement,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetLifecycleEntity ToEntity(string lifecycleId)
    {
        return new AssetLifecycleEntity
        {
            LifecycleId = lifecycleId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            ProcurementDate = ProcurementDate,
            DeploymentDate = DeploymentDate,
            LifecycleStatus = string.IsNullOrWhiteSpace(Status) ? "Active" : Status,
            DisposalDetails = DisposalDetails ?? string.Empty,
            ReasonForRetirement = ReasonForRetirement ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetLifecycleRequest(
    string? AssetId,
    string? AssetName,
    DateTime? ProcurementDate,
    DateTime? DeploymentDate,
    string? Status,
    string? DisposalDetails,
    string? ReasonForRetirement,
    string? UpdatedBy)
{
    public void ApplyTo(AssetLifecycleEntity lifecycle)
    {
        lifecycle.AssetId = AssetId ?? string.Empty;
        lifecycle.AssetName = AssetName ?? string.Empty;
        lifecycle.ProcurementDate = ProcurementDate;
        lifecycle.DeploymentDate = DeploymentDate;
        lifecycle.LifecycleStatus = string.IsNullOrWhiteSpace(Status) ? "Active" : Status;
        lifecycle.DisposalDetails = DisposalDetails ?? string.Empty;
        lifecycle.ReasonForRetirement = ReasonForRetirement ?? string.Empty;
        lifecycle.UpdatedBy = UpdatedBy;
        lifecycle.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetLifecycleResponse(
    string Id,
    string LifecycleId,
    string AssetId,
    string AssetName,
    DateTime? ProcurementDate,
    DateTime? DeploymentDate,
    string Status,
    string DisposalDetails,
    string ReasonForRetirement,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetLifecycleResponse FromEntity(AssetLifecycleEntity lifecycle)
    {
        return new AssetLifecycleResponse(
            lifecycle.Id,
            lifecycle.LifecycleId,
            lifecycle.AssetId,
            lifecycle.AssetName,
            lifecycle.ProcurementDate,
            lifecycle.DeploymentDate,
            lifecycle.LifecycleStatus,
            lifecycle.DisposalDetails,
            lifecycle.ReasonForRetirement,
            lifecycle.CreatedBy,
            lifecycle.CreatedAt,
            lifecycle.UpdatedBy,
            lifecycle.UpdatedAt,
            lifecycle.ClientId,
            lifecycle.TenantId,
            lifecycle.IsDeleted);
    }
}
