using ResourceTypeEntity = A360.MasterManagement.Domain.Entities.ResourceType;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateResourceTypeRequest(
    string? AssetId,
    string? TypeName,
    string? Category,
    bool IsActive,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public ResourceTypeEntity ToEntity(string typeId, string assetName)
    {
        return new ResourceTypeEntity
        {
            TypeId = typeId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            TypeName = TypeName ?? string.Empty,
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

public sealed record UpdateResourceTypeRequest(
    string? AssetId,
    string? TypeName,
    string? Category,
    bool IsActive,
    string? UpdatedBy)
{
    public void ApplyTo(ResourceTypeEntity resourceType, string assetName)
    {
        resourceType.AssetId = AssetId ?? string.Empty;
        resourceType.AssetName = assetName;
        resourceType.TypeName = TypeName ?? string.Empty;
        resourceType.Category = Category ?? string.Empty;
        resourceType.IsActive = IsActive;
        resourceType.UpdatedBy = UpdatedBy;
        resourceType.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record ResourceTypeResponse(
    string Id,
    string TypeId,
    string AssetId,
    string AssetName,
    string TypeName,
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
    public static ResourceTypeResponse FromEntity(ResourceTypeEntity resourceType)
    {
        return new ResourceTypeResponse(
            resourceType.Id,
            resourceType.TypeId,
            resourceType.AssetId,
            resourceType.AssetName,
            resourceType.TypeName,
            resourceType.Category,
            resourceType.IsActive,
            resourceType.CreatedBy,
            resourceType.CreatedAt,
            resourceType.UpdatedBy,
            resourceType.UpdatedAt,
            resourceType.ClientId,
            resourceType.TenantId,
            resourceType.IsDeleted);
    }
}
