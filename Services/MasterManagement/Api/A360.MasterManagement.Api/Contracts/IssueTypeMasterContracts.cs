using IssueTypeMasterEntity = A360.MasterManagement.Domain.Entities.IssueTypeMaster;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateIssueTypeMasterRequest(
    string? AssetId,
    string? IssueTypeName,
    string? Category,
    bool IsActive,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public IssueTypeMasterEntity ToEntity(string issueTypeId, string assetName)
    {
        return new IssueTypeMasterEntity
        {
            IssueTypeId = issueTypeId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            IssueTypeName = IssueTypeName ?? string.Empty,
            Category = Category ?? string.Empty,
            IsActive = IsActive,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateIssueTypeMasterRequest(
    string? AssetId,
    string? IssueTypeName,
    string? Category,
    bool IsActive,
    string? UpdatedBy)
{
    public void ApplyTo(IssueTypeMasterEntity issueTypeMaster, string assetName)
    {
        issueTypeMaster.AssetId = AssetId ?? string.Empty;
        issueTypeMaster.AssetName = assetName;
        issueTypeMaster.IssueTypeName = IssueTypeName ?? string.Empty;
        issueTypeMaster.Category = Category ?? string.Empty;
        issueTypeMaster.IsActive = IsActive;
        issueTypeMaster.UpdatedBy = UpdatedBy;
        issueTypeMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record IssueTypeMasterResponse(
    string Id,
    string IssueTypeId,
    string AssetId,
    string AssetName,
    string IssueTypeName,
    string Category,
    bool IsActive,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static IssueTypeMasterResponse FromEntity(IssueTypeMasterEntity issueTypeMaster)
    {
        return new IssueTypeMasterResponse(
            issueTypeMaster.Id,
            issueTypeMaster.IssueTypeId,
            issueTypeMaster.AssetId,
            issueTypeMaster.AssetName,
            issueTypeMaster.IssueTypeName,
            issueTypeMaster.Category,
            issueTypeMaster.IsActive,
            issueTypeMaster.CreatedBy,
            issueTypeMaster.CreatedAt,
            issueTypeMaster.UpdatedBy,
            issueTypeMaster.UpdatedAt,
            issueTypeMaster.ClientId,
            issueTypeMaster.TenantId,
            issueTypeMaster.IsDeleted);
    }
}
