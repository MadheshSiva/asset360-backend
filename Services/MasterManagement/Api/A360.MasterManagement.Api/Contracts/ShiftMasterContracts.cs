using ShiftMasterEntity = A360.MasterManagement.Domain.Entities.ShiftMaster;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateShiftMasterRequest(
    string? AssetId,
    string? ShiftName,
    string? StartTime,
    string? EndTime,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public ShiftMasterEntity ToEntity(string shiftId, string assetName)
    {
        return new ShiftMasterEntity
        {
            ShiftId = shiftId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            ShiftName = ShiftName ?? string.Empty,
            StartTime = StartTime ?? string.Empty,
            EndTime = EndTime ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateShiftMasterRequest(
    string? AssetId,
    string? ShiftName,
    string? StartTime,
    string? EndTime,
    string? UpdatedBy)
{
    public void ApplyTo(ShiftMasterEntity shiftMaster, string assetName)
    {
        shiftMaster.AssetId = AssetId ?? string.Empty;
        shiftMaster.AssetName = assetName;
        shiftMaster.ShiftName = ShiftName ?? string.Empty;
        shiftMaster.StartTime = StartTime ?? string.Empty;
        shiftMaster.EndTime = EndTime ?? string.Empty;
        shiftMaster.UpdatedBy = UpdatedBy;
        shiftMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record ShiftMasterResponse(
    string Id,
    string ShiftId,
    string AssetId,
    string AssetName,
    string ShiftName,
    string StartTime,
    string EndTime,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static ShiftMasterResponse FromEntity(ShiftMasterEntity shiftMaster)
    {
        return new ShiftMasterResponse(
            shiftMaster.Id,
            shiftMaster.ShiftId,
            shiftMaster.AssetId,
            shiftMaster.AssetName,
            shiftMaster.ShiftName,
            shiftMaster.StartTime,
            shiftMaster.EndTime,
            shiftMaster.CreatedBy,
            shiftMaster.CreatedAt,
            shiftMaster.UpdatedBy,
            shiftMaster.UpdatedAt,
            shiftMaster.ClientId,
            shiftMaster.TenantId,
            shiftMaster.IsDeleted);
    }
}
