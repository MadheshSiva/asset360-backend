using AssetIntegrationEntity = A360.Asset.Domain.Entities.AssetIntegration;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetIntegrationRequest(
    string? AssetId,
    string? AssetName,
    string? ErpId,
    string? WmsReference,
    string? ApiSyncStatus,
    DateTime? LastSyncTimestamp,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetIntegrationEntity ToEntity(string integrationId)
    {
        return new AssetIntegrationEntity
        {
            IntegrationId = integrationId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            ErpId = ErpId ?? string.Empty,
            WmsReference = WmsReference ?? string.Empty,
            ApiSyncStatus = ApiSyncStatus ?? string.Empty,
            LastSyncTimestamp = LastSyncTimestamp,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetIntegrationRequest(
    string? AssetId,
    string? AssetName,
    string? ErpId,
    string? WmsReference,
    string? ApiSyncStatus,
    DateTime? LastSyncTimestamp,
    string? UpdatedBy)
{
    public void ApplyTo(AssetIntegrationEntity integration)
    {
        integration.AssetId = AssetId ?? string.Empty;
        integration.AssetName = AssetName ?? string.Empty;
        integration.ErpId = ErpId ?? string.Empty;
        integration.WmsReference = WmsReference ?? string.Empty;
        integration.ApiSyncStatus = ApiSyncStatus ?? string.Empty;
        integration.LastSyncTimestamp = LastSyncTimestamp;
        integration.UpdatedBy = UpdatedBy;
        integration.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetIntegrationResponse(
    string Id,
    string IntegrationId,
    string AssetId,
    string AssetName,
    string ErpId,
    string WmsReference,
    string ApiSyncStatus,
    DateTime? LastSyncTimestamp,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetIntegrationResponse FromEntity(AssetIntegrationEntity integration)
    {
        return new AssetIntegrationResponse(
            integration.Id,
            integration.IntegrationId,
            integration.AssetId,
            integration.AssetName,
            integration.ErpId,
            integration.WmsReference,
            integration.ApiSyncStatus,
            integration.LastSyncTimestamp,
            integration.CreatedBy,
            integration.CreatedAt,
            integration.UpdatedBy,
            integration.UpdatedAt,
            integration.ClientId,
            integration.TenantId,
            integration.IsDeleted);
    }
}
