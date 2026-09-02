using AssetMovementEntity = A360.Asset.Domain.Entities.AssetMovement;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetMovementRequest(
    string? AssetId,
    string? AssetName,
    string? ReferenceNumber,
    string? Status,
    DateTime? MovementDate,
    string? LastApprovalWorkflow,
    string? NextApprovalWorkflow,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetMovementEntity ToEntity(string movementId)
    {
        return new AssetMovementEntity
        {
            MovementId = movementId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            ReferenceNumber = ReferenceNumber ?? string.Empty,
            MovementStatus = Status ?? string.Empty,
            MovementDate = MovementDate,
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

public sealed record UpdateAssetMovementRequest(
    string? AssetId,
    string? AssetName,
    string? ReferenceNumber,
    string? Status,
    DateTime? MovementDate,
    string? LastApprovalWorkflow,
    string? NextApprovalWorkflow,
    string? UpdatedBy)
{
    public void ApplyTo(AssetMovementEntity movement)
    {
        movement.AssetId = AssetId ?? string.Empty;
        movement.AssetName = AssetName ?? string.Empty;
        movement.ReferenceNumber = ReferenceNumber ?? string.Empty;
        movement.MovementStatus = Status ?? string.Empty;
        movement.MovementDate = MovementDate;
        movement.LastApprovalWorkflow = LastApprovalWorkflow ?? string.Empty;
        movement.NextApprovalWorkflow = NextApprovalWorkflow ?? string.Empty;
        movement.UpdatedBy = UpdatedBy;
        movement.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetMovementResponse(
    string Id,
    string MovementId,
    string AssetId,
    string AssetName,
    string ReferenceNumber,
    string Status,
    DateTime? MovementDate,
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
    public static AssetMovementResponse FromEntity(AssetMovementEntity movement)
    {
        return new AssetMovementResponse(
            movement.Id,
            movement.MovementId,
            movement.AssetId,
            movement.AssetName,
            movement.ReferenceNumber,
            movement.MovementStatus,
            movement.MovementDate,
            movement.LastApprovalWorkflow,
            movement.NextApprovalWorkflow,
            movement.CreatedBy,
            movement.CreatedAt,
            movement.UpdatedBy,
            movement.UpdatedAt,
            movement.ClientId,
            movement.TenantId,
            movement.IsDeleted);
    }
}
