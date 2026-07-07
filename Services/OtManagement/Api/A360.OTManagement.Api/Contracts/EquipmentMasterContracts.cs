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
    string? CreatedBy)
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
            CreatedAt = DateTime.UtcNow
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
    bool Status)
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
    string CreatedBy,
    DateTime CreatedAt)
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
            equipmentMaster.CreatedBy ?? string.Empty,
            equipmentMaster.CreatedAt);
    }
}
