using AssetDisposalEntity = A360.Asset.Domain.Entities.AssetDisposal;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetDisposalRequest(
    string? AssetId,
    string? AssetName,
    string? ReferenceNumber,
    string? RequestedBy,
    string? DisposalReason,
    string? Status,
    DateTime? DisposalDate,
    string? LastApprovalWorkflow,
    string? NextApprovalWorkflow,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetDisposalEntity ToEntity(string disposalId)
    {
        return new AssetDisposalEntity
        {
            DisposalId = disposalId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            ReferenceNumber = ReferenceNumber ?? string.Empty,
            RequestedBy = RequestedBy ?? string.Empty,
            DisposalReason = DisposalReason ?? string.Empty,
            DisposalStatus = Status ?? string.Empty,
            DisposalDate = DisposalDate,
            LastApprovalWorkflow = LastApprovalWorkflow ?? string.Empty,
            NextApprovalWorkflow = NextApprovalWorkflow ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetDisposalRequest(
    string? AssetId,
    string? AssetName,
    string? ReferenceNumber,
    string? RequestedBy,
    string? DisposalReason,
    string? Status,
    DateTime? DisposalDate,
    string? LastApprovalWorkflow,
    string? NextApprovalWorkflow,
    string? UpdatedBy)
{
    public void ApplyTo(AssetDisposalEntity disposal)
    {
        disposal.AssetId = AssetId ?? string.Empty;
        disposal.AssetName = AssetName ?? string.Empty;
        disposal.ReferenceNumber = ReferenceNumber ?? string.Empty;
        disposal.RequestedBy = RequestedBy ?? string.Empty;
        disposal.DisposalReason = DisposalReason ?? string.Empty;
        disposal.DisposalStatus = Status ?? string.Empty;
        disposal.DisposalDate = DisposalDate;
        disposal.LastApprovalWorkflow = LastApprovalWorkflow ?? string.Empty;
        disposal.NextApprovalWorkflow = NextApprovalWorkflow ?? string.Empty;
        disposal.UpdatedBy = UpdatedBy;
        disposal.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetDisposalResponse(
    string Id,
    string DisposalId,
    string AssetId,
    string AssetName,
    string ReferenceNumber,
    string RequestedBy,
    string DisposalReason,
    string Status,
    DateTime? DisposalDate,
    string LastApprovalWorkflow,
    string NextApprovalWorkflow,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetDisposalResponse FromEntity(AssetDisposalEntity disposal)
    {
        return new AssetDisposalResponse(
            disposal.Id,
            disposal.DisposalId,
            disposal.AssetId,
            disposal.AssetName,
            disposal.ReferenceNumber,
            disposal.RequestedBy,
            disposal.DisposalReason,
            disposal.DisposalStatus,
            disposal.DisposalDate,
            disposal.LastApprovalWorkflow,
            disposal.NextApprovalWorkflow,
            disposal.CreatedBy,
            disposal.CreatedAt,
            disposal.UpdatedBy,
            disposal.UpdatedAt,
            disposal.ClientId,
            disposal.TenantId,
            disposal.IsDeleted);
    }
}
