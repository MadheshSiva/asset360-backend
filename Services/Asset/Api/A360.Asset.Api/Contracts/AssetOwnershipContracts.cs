using AssetOwnershipEntity = A360.Asset.Domain.Entities.AssetOwnership;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetOwnershipRequest(
    string? AssetId,
    string? AssetName,
    string? AssignedCustodian,
    string? Department,
    DateTime? AssignmentStartDate,
    DateTime? AssignmentEndDate,
    string? TransferHistory,
    string? CustodianDetails,
    string? CheckInOutLogs,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetOwnershipEntity ToEntity(string ownershipId)
    {
        return new AssetOwnershipEntity
        {
            OwnershipId = ownershipId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            AssignedCustodian = AssignedCustodian ?? string.Empty,
            Department = Department ?? string.Empty,
            AssignmentStartDate = AssignmentStartDate,
            AssignmentEndDate = AssignmentEndDate,
            TransferHistory = TransferHistory ?? string.Empty,
            CustodianDetails = CustodianDetails ?? string.Empty,
            CheckInOutLogs = CheckInOutLogs ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetOwnershipRequest(
    string? AssetId,
    string? AssetName,
    string? AssignedCustodian,
    string? Department,
    DateTime? AssignmentStartDate,
    DateTime? AssignmentEndDate,
    string? TransferHistory,
    string? CustodianDetails,
    string? CheckInOutLogs,
    string? UpdatedBy)
{
    public void ApplyTo(AssetOwnershipEntity ownership)
    {
        ownership.AssetId = AssetId ?? string.Empty;
        ownership.AssetName = AssetName ?? string.Empty;
        ownership.AssignedCustodian = AssignedCustodian ?? string.Empty;
        ownership.Department = Department ?? string.Empty;
        ownership.AssignmentStartDate = AssignmentStartDate;
        ownership.AssignmentEndDate = AssignmentEndDate;
        ownership.TransferHistory = TransferHistory ?? string.Empty;
        ownership.CustodianDetails = CustodianDetails ?? string.Empty;
        ownership.CheckInOutLogs = CheckInOutLogs ?? string.Empty;
        ownership.UpdatedBy = UpdatedBy;
        ownership.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetOwnershipResponse(
    string Id,
    string OwnershipId,
    string AssetId,
    string AssetName,
    string AssignedCustodian,
    string Department,
    DateTime? AssignmentStartDate,
    DateTime? AssignmentEndDate,
    string TransferHistory,
    string CustodianDetails,
    string CheckInOutLogs,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetOwnershipResponse FromEntity(AssetOwnershipEntity ownership)
    {
        return new AssetOwnershipResponse(
            ownership.Id,
            ownership.OwnershipId,
            ownership.AssetId,
            ownership.AssetName,
            ownership.AssignedCustodian,
            ownership.Department,
            ownership.AssignmentStartDate,
            ownership.AssignmentEndDate,
            ownership.TransferHistory,
            ownership.CustodianDetails,
            ownership.CheckInOutLogs,
            ownership.CreatedBy,
            ownership.CreatedAt,
            ownership.UpdatedBy,
            ownership.UpdatedAt,
            ownership.ClientId,
            ownership.TenantId,
            ownership.IsDeleted);
    }
}
