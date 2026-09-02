using AssetActivityEntity = A360.Asset.Domain.Entities.AssetActivity;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetActivityRequest(
    string? AssetId,
    string? AssetName,
    string? WhoCreatedUpdatedAsset,
    string? ChangesMade,
    string? TimestampLogs,
    string? AccessLogs,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetActivityEntity ToEntity(string activityId)
    {
        return new AssetActivityEntity
        {
            ActivityId = activityId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            WhoCreatedUpdatedAsset = WhoCreatedUpdatedAsset ?? string.Empty,
            ChangesMade = ChangesMade ?? string.Empty,
            TimestampLogs = TimestampLogs ?? string.Empty,
            AccessLogs = AccessLogs ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetActivityRequest(
    string? AssetId,
    string? AssetName,
    string? WhoCreatedUpdatedAsset,
    string? ChangesMade,
    string? TimestampLogs,
    string? AccessLogs,
    string? UpdatedBy)
{
    public void ApplyTo(AssetActivityEntity activity)
    {
        activity.AssetId = AssetId ?? string.Empty;
        activity.AssetName = AssetName ?? string.Empty;
        activity.WhoCreatedUpdatedAsset = WhoCreatedUpdatedAsset ?? string.Empty;
        activity.ChangesMade = ChangesMade ?? string.Empty;
        activity.TimestampLogs = TimestampLogs ?? string.Empty;
        activity.AccessLogs = AccessLogs ?? string.Empty;
        activity.UpdatedBy = UpdatedBy;
        activity.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetActivityResponse(
    string Id,
    string ActivityId,
    string AssetId,
    string AssetName,
    string WhoCreatedUpdatedAsset,
    string ChangesMade,
    string TimestampLogs,
    string AccessLogs,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetActivityResponse FromEntity(AssetActivityEntity activity)
    {
        return new AssetActivityResponse(
            activity.Id,
            activity.ActivityId,
            activity.AssetId,
            activity.AssetName,
            activity.WhoCreatedUpdatedAsset,
            activity.ChangesMade,
            activity.TimestampLogs,
            activity.AccessLogs,
            activity.CreatedBy,
            activity.CreatedAt,
            activity.UpdatedBy,
            activity.UpdatedAt,
            activity.ClientId,
            activity.TenantId,
            activity.IsDeleted);
    }
}
