using ConditionMasterEntity = A360.MasterManagement.Domain.Entities.ConditionMaster;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateConditionMasterRequest(
    string? AssetId,
    string? ConditionName,
    double ThresholdValue,
    string? ColorCode,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public ConditionMasterEntity ToEntity(string conditionId, string assetName)
    {
        return new ConditionMasterEntity
        {
            ConditionId = conditionId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            ConditionName = ConditionName ?? string.Empty,
            ThresholdValue = ThresholdValue,
            ColorCode = ColorCode ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateConditionMasterRequest(
    string? AssetId,
    string? ConditionName,
    double ThresholdValue,
    string? ColorCode,
    string? UpdatedBy)
{
    public void ApplyTo(ConditionMasterEntity conditionMaster, string assetName)
    {
        conditionMaster.AssetId = AssetId ?? string.Empty;
        conditionMaster.AssetName = assetName;
        conditionMaster.ConditionName = ConditionName ?? string.Empty;
        conditionMaster.ThresholdValue = ThresholdValue;
        conditionMaster.ColorCode = ColorCode ?? string.Empty;
        conditionMaster.UpdatedBy = UpdatedBy;
        conditionMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record ConditionMasterResponse(
    string Id,
    string ConditionId,
    string AssetId,
    string AssetName,
    string ConditionName,
    double ThresholdValue,
    string ColorCode,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static ConditionMasterResponse FromEntity(ConditionMasterEntity conditionMaster)
    {
        return new ConditionMasterResponse(
            conditionMaster.Id,
            conditionMaster.ConditionId,
            conditionMaster.AssetId,
            conditionMaster.AssetName,
            conditionMaster.ConditionName,
            conditionMaster.ThresholdValue,
            conditionMaster.ColorCode,
            conditionMaster.CreatedBy,
            conditionMaster.CreatedAt,
            conditionMaster.UpdatedBy,
            conditionMaster.UpdatedAt,
            conditionMaster.ClientId,
            conditionMaster.TenantId,
            conditionMaster.IsDeleted);
    }
}
