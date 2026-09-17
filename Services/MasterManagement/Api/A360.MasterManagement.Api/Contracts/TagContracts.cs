using TagEntity = A360.MasterManagement.Domain.Entities.Tag;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateTagRequest(
    string? AssetId,
    string? TagCode,
    string? TagType,
    string? AssignedAssetCode,
    DateTime? IssueDate,
    bool Active,
    string? CreatedBy,
    string? ClientId,
    string? TenantId,
    string? Status)
{
    public TagEntity ToEntity(string tagId, string assetName)
    {
        return new TagEntity
        {
            TagId = tagId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            TagCode = TagCode ?? string.Empty,
            TagType = TagType ?? string.Empty,
            AssignedAssetCode = AssignedAssetCode ?? string.Empty,
            IssueDate = IssueDate,
            Active = Active,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            Status = Status,
            IsDeleted = false
        };
    }
}

public sealed record UpdateTagRequest(
    string? AssetId,
    string? TagCode,
    string? TagType,
    string? AssignedAssetCode,
    DateTime? IssueDate,
    bool Active,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(TagEntity tag, string assetName)
    {
        tag.AssetId = AssetId ?? string.Empty;
        tag.AssetName = assetName;
        tag.TagCode = TagCode ?? string.Empty;
        tag.TagType = TagType ?? string.Empty;
        tag.AssignedAssetCode = AssignedAssetCode ?? string.Empty;
        tag.IssueDate = IssueDate;
        tag.Active = Active;
        tag.UpdatedBy = UpdatedBy;
        tag.Status = Status;
        tag.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record TagResponse(
    string Id,
    string TagId,
    string AssetId,
    string AssetName,
    string TagCode,
    string TagType,
    string AssignedAssetCode,
    DateTime? IssueDate,
    bool Active,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted,
    string? Status)
{
    public static TagResponse FromEntity(TagEntity tag)
    {
        return new TagResponse(
            tag.Id,
            tag.TagId,
            tag.AssetId,
            tag.AssetName,
            tag.TagCode,
            tag.TagType,
            tag.AssignedAssetCode,
            tag.IssueDate,
            tag.Active,
            tag.CreatedBy,
            tag.CreatedAt,
            tag.UpdatedBy,
            tag.UpdatedAt,
            tag.ClientId,
            tag.TenantId,
            tag.IsDeleted,
            tag.Status);
    }
}
