using EquipmentMasterEntity = A360.OTManagement.Domain.Entities.EquipmentMaster;

namespace A360.OTManagement.Api.Contracts;

public sealed record CreateEquipmentMasterRequest(
    string? AssetId,
    string? EquipmentName,
    string? Type,
    string? SerialNumber,
    string? Location,
    string? TagId,
    DateTime ServiceDate,
    bool Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public EquipmentMasterEntity ToEntity()
    {
        return new EquipmentMasterEntity
        {
            AssetId = AssetId,
            EquipmentName = EquipmentName,
            Type = Type,
            SerialNumber = SerialNumber,
            Location = Location,
            TagId = TagId,
            ServiceDate = ServiceDate,
            Status = Status,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateEquipmentMasterRequest(
    string? EquipmentName,
    string? Type,
    string? SerialNumber,
    string? Location,
    string? TagId,
    DateTime ServiceDate,
    bool Status,
    string? UpdatedBy)
{
    public void ApplyTo(
        EquipmentMasterEntity equipmentMaster)
    {
        equipmentMaster.EquipmentName = EquipmentName;
        equipmentMaster.Type = Type;
        equipmentMaster.SerialNumber = SerialNumber;
        equipmentMaster.Location = Location;
        equipmentMaster.TagId = TagId;
        equipmentMaster.ServiceDate = ServiceDate;
        equipmentMaster.Status = Status;
        equipmentMaster.UpdatedBy = UpdatedBy;
        equipmentMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record EquipmentMasterResponse(
    string Id,
    string AssetId,
    string EquipmentName,
    string Type,
    string SerialNumber,
    string Location,
    string TagId,
    DateTime ServiceDate,
    bool Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static EquipmentMasterResponse FromEntity(
        EquipmentMasterEntity equipmentMaster)
    {
        return new EquipmentMasterResponse(
            equipmentMaster.Id ?? string.Empty,
            equipmentMaster.AssetId ?? string.Empty,
            equipmentMaster.EquipmentName ?? string.Empty,
            equipmentMaster.Type ?? string.Empty,
            equipmentMaster.SerialNumber ?? string.Empty,
            equipmentMaster.Location ?? string.Empty,
            equipmentMaster.TagId ?? string.Empty,
            equipmentMaster.ServiceDate,
            equipmentMaster.Status,
            equipmentMaster.CreatedBy,
            equipmentMaster.CreatedAt,
            equipmentMaster.UpdatedBy,
            equipmentMaster.UpdatedAt,
            equipmentMaster.ClientId,
            equipmentMaster.TenantId,
            equipmentMaster.IsDeleted);
    }
}
