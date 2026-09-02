using AssetMaintenanceAndServiceEntity = A360.Asset.Domain.Entities.AssetMaintenanceAndService;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetMaintenanceAndServiceRequest(
    string? AssetId,
    string? AssetName,
    string? MaintenanceSchedule,
    string? WorkOrders,
    string? ServiceHistory,
    string? RepairLogs,
    string? DowntimeDuration,
    string? SparePartsUsed,
    string? VendorServiceProviderDetails,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetMaintenanceAndServiceEntity ToEntity(string maintenanceServiceId)
    {
        return new AssetMaintenanceAndServiceEntity
        {
            MaintenanceServiceId = maintenanceServiceId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            MaintenanceSchedule = MaintenanceSchedule ?? string.Empty,
            WorkOrders = WorkOrders ?? string.Empty,
            ServiceHistory = ServiceHistory ?? string.Empty,
            RepairLogs = RepairLogs ?? string.Empty,
            DowntimeDuration = DowntimeDuration ?? string.Empty,
            SparePartsUsed = SparePartsUsed ?? string.Empty,
            VendorServiceProviderDetails = VendorServiceProviderDetails ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetMaintenanceAndServiceRequest(
    string? AssetId,
    string? AssetName,
    string? MaintenanceSchedule,
    string? WorkOrders,
    string? ServiceHistory,
    string? RepairLogs,
    string? DowntimeDuration,
    string? SparePartsUsed,
    string? VendorServiceProviderDetails,
    string? UpdatedBy)
{
    public void ApplyTo(AssetMaintenanceAndServiceEntity maintenanceService)
    {
        maintenanceService.AssetId = AssetId ?? string.Empty;
        maintenanceService.AssetName = AssetName ?? string.Empty;
        maintenanceService.MaintenanceSchedule = MaintenanceSchedule ?? string.Empty;
        maintenanceService.WorkOrders = WorkOrders ?? string.Empty;
        maintenanceService.ServiceHistory = ServiceHistory ?? string.Empty;
        maintenanceService.RepairLogs = RepairLogs ?? string.Empty;
        maintenanceService.DowntimeDuration = DowntimeDuration ?? string.Empty;
        maintenanceService.SparePartsUsed = SparePartsUsed ?? string.Empty;
        maintenanceService.VendorServiceProviderDetails = VendorServiceProviderDetails ?? string.Empty;
        maintenanceService.UpdatedBy = UpdatedBy;
        maintenanceService.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetMaintenanceAndServiceResponse(
    string Id,
    string MaintenanceServiceId,
    string AssetId,
    string AssetName,
    string MaintenanceSchedule,
    string WorkOrders,
    string ServiceHistory,
    string RepairLogs,
    string DowntimeDuration,
    string SparePartsUsed,
    string VendorServiceProviderDetails,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetMaintenanceAndServiceResponse FromEntity(AssetMaintenanceAndServiceEntity maintenanceService)
    {
        return new AssetMaintenanceAndServiceResponse(
            maintenanceService.Id,
            maintenanceService.MaintenanceServiceId,
            maintenanceService.AssetId,
            maintenanceService.AssetName,
            maintenanceService.MaintenanceSchedule,
            maintenanceService.WorkOrders,
            maintenanceService.ServiceHistory,
            maintenanceService.RepairLogs,
            maintenanceService.DowntimeDuration,
            maintenanceService.SparePartsUsed,
            maintenanceService.VendorServiceProviderDetails,
            maintenanceService.CreatedBy,
            maintenanceService.CreatedAt,
            maintenanceService.UpdatedBy,
            maintenanceService.UpdatedAt,
            maintenanceService.ClientId,
            maintenanceService.TenantId,
            maintenanceService.IsDeleted);
    }
}
