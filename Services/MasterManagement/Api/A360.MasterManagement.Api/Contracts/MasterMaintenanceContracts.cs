using MasterMaintenanceEntity = A360.MasterManagement.Domain.Entities.MasterMaintenance;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateMasterMaintenanceRequest(
    string? AssetId,
    string? MasterMaintenanceCode,
    string? MasterMaintenanceName,
    string? Category,
    string? Frequency,
    double StandardDurationHrs,
    bool Active,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public MasterMaintenanceEntity ToEntity(string masterMaintenanceId, string assetName)
    {
        return new MasterMaintenanceEntity
        {
            MasterMaintenanceId = masterMaintenanceId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            MasterMaintenanceCode = MasterMaintenanceCode ?? string.Empty,
            MasterMaintenanceName = MasterMaintenanceName ?? string.Empty,
            Category = Category ?? string.Empty,
            Frequency = Frequency ?? string.Empty,
            StandardDurationHrs = StandardDurationHrs,
            Active = Active,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateMasterMaintenanceRequest(
    string? AssetId,
    string? MasterMaintenanceCode,
    string? MasterMaintenanceName,
    string? Category,
    string? Frequency,
    double StandardDurationHrs,
    bool Active,
    string? UpdatedBy)
{
    public void ApplyTo(MasterMaintenanceEntity masterMaintenance, string assetName)
    {
        masterMaintenance.AssetId = AssetId ?? string.Empty;
        masterMaintenance.AssetName = assetName;
        masterMaintenance.MasterMaintenanceCode = MasterMaintenanceCode ?? string.Empty;
        masterMaintenance.MasterMaintenanceName = MasterMaintenanceName ?? string.Empty;
        masterMaintenance.Category = Category ?? string.Empty;
        masterMaintenance.Frequency = Frequency ?? string.Empty;
        masterMaintenance.StandardDurationHrs = StandardDurationHrs;
        masterMaintenance.Active = Active;
        masterMaintenance.UpdatedBy = UpdatedBy;
        masterMaintenance.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record MasterMaintenanceResponse(
    string Id,
    string MasterMaintenanceId,
    string AssetId,
    string AssetName,
    string MasterMaintenanceCode,
    string MasterMaintenanceName,
    string Category,
    string Frequency,
    double StandardDurationHrs,
    bool Active,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static MasterMaintenanceResponse FromEntity(MasterMaintenanceEntity masterMaintenance)
    {
        return new MasterMaintenanceResponse(
            masterMaintenance.Id,
            masterMaintenance.MasterMaintenanceId,
            masterMaintenance.AssetId,
            masterMaintenance.AssetName,
            masterMaintenance.MasterMaintenanceCode,
            masterMaintenance.MasterMaintenanceName,
            masterMaintenance.Category,
            masterMaintenance.Frequency,
            masterMaintenance.StandardDurationHrs,
            masterMaintenance.Active,
            masterMaintenance.CreatedBy,
            masterMaintenance.CreatedAt,
            masterMaintenance.UpdatedBy,
            masterMaintenance.UpdatedAt,
            masterMaintenance.ClientId,
            masterMaintenance.TenantId,
            masterMaintenance.IsDeleted);
    }
}
