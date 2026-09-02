using UnitMasterEntity = A360.MasterManagement.Domain.Entities.UnitMaster;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateUnitMasterRequest(
    string? AssetId,
    string? UnitName,
    string? Symbol,
    bool IsActive,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public UnitMasterEntity ToEntity(string unitId, string assetName)
    {
        return new UnitMasterEntity
        {
            UnitId = unitId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            UnitName = UnitName ?? string.Empty,
            Symbol = Symbol ?? string.Empty,
            IsActive = IsActive,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateUnitMasterRequest(
    string? AssetId,
    string? UnitName,
    string? Symbol,
    bool IsActive,
    string? UpdatedBy)
{
    public void ApplyTo(UnitMasterEntity unitMaster, string assetName)
    {
        unitMaster.AssetId = AssetId ?? string.Empty;
        unitMaster.AssetName = assetName;
        unitMaster.UnitName = UnitName ?? string.Empty;
        unitMaster.Symbol = Symbol ?? string.Empty;
        unitMaster.IsActive = IsActive;
        unitMaster.UpdatedBy = UpdatedBy;
        unitMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record UnitMasterResponse(
    string Id,
    string UnitId,
    string AssetId,
    string AssetName,
    string UnitName,
    string Symbol,
    bool IsActive,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static UnitMasterResponse FromEntity(UnitMasterEntity unitMaster)
    {
        return new UnitMasterResponse(
            unitMaster.Id,
            unitMaster.UnitId,
            unitMaster.AssetId,
            unitMaster.AssetName,
            unitMaster.UnitName,
            unitMaster.Symbol,
            unitMaster.IsActive,
            unitMaster.CreatedBy,
            unitMaster.CreatedAt,
            unitMaster.UpdatedBy,
            unitMaster.UpdatedAt,
            unitMaster.ClientId,
            unitMaster.TenantId,
            unitMaster.IsDeleted);
    }
}
