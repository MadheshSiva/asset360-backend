using AlertTypeEntity = A360.MasterManagement.Domain.Entities.AlertType;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateAlertTypeRequest(
    string? AssetId,
    string? AlertName,
    string? AlertCode,
    string? Description,
    string? Category,
    string? Severity,
    string? TriggerCondition,
    string? NotificationType,
    string? Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AlertTypeEntity ToEntity(string alertTypeId, string assetName)
    {
        return new AlertTypeEntity
        {
            AlertTypeId = alertTypeId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            AlertName = AlertName ?? string.Empty,
            AlertCode = AlertCode ?? string.Empty,
            Description = Description ?? string.Empty,
            Category = Category ?? string.Empty,
            Severity = Severity ?? string.Empty,
            TriggerCondition = TriggerCondition ?? string.Empty,
            NotificationType = NotificationType ?? string.Empty,
            Status = Status,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAlertTypeRequest(
    string? AssetId,
    string? AlertName,
    string? AlertCode,
    string? Description,
    string? Category,
    string? Severity,
    string? TriggerCondition,
    string? NotificationType,
    string? Status,
    string? UpdatedBy)
{
    public void ApplyTo(AlertTypeEntity alertType, string assetName)
    {
        alertType.AssetId = AssetId ?? string.Empty;
        alertType.AssetName = assetName;
        alertType.AlertName = AlertName ?? string.Empty;
        alertType.AlertCode = AlertCode ?? string.Empty;
        alertType.Description = Description ?? string.Empty;
        alertType.Category = Category ?? string.Empty;
        alertType.Severity = Severity ?? string.Empty;
        alertType.TriggerCondition = TriggerCondition ?? string.Empty;
        alertType.NotificationType = NotificationType ?? string.Empty;
        alertType.Status = Status;
        alertType.UpdatedBy = UpdatedBy;
        alertType.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AlertTypeResponse(
    string Id,
    string AlertTypeId,
    string AssetId,
    string AssetName,
    string AlertName,
    string AlertCode,
    string Description,
    string Category,
    string Severity,
    string TriggerCondition,
    string NotificationType,
    string? Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AlertTypeResponse FromEntity(AlertTypeEntity alertType)
    {
        return new AlertTypeResponse(
            alertType.Id,
            alertType.AlertTypeId,
            alertType.AssetId,
            alertType.AssetName,
            alertType.AlertName,
            alertType.AlertCode,
            alertType.Description,
            alertType.Category,
            alertType.Severity,
            alertType.TriggerCondition,
            alertType.NotificationType,
            alertType.Status,
            alertType.CreatedBy,
            alertType.CreatedAt,
            alertType.UpdatedBy,
            alertType.UpdatedAt,
            alertType.ClientId,
            alertType.TenantId,
            alertType.IsDeleted);
    }
}
