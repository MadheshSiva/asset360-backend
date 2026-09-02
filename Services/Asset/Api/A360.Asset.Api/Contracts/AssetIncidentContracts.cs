using AssetIncidentEntity = A360.Asset.Domain.Entities.AssetIncident;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetIncidentRequest(
    string? AssetId,
    string? AssetName,
    string? AlertType,
    string? IncidentReports,
    string? DamageReports,
    string? TheftLossRecords,
    string? ResolutionStatus,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetIncidentEntity ToEntity(string incidentId)
    {
        return new AssetIncidentEntity
        {
            IncidentId = incidentId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            AlertType = AlertType ?? string.Empty,
            IncidentReports = IncidentReports ?? string.Empty,
            DamageReports = DamageReports ?? string.Empty,
            TheftLossRecords = TheftLossRecords ?? string.Empty,
            ResolutionStatus = ResolutionStatus ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetIncidentRequest(
    string? AssetId,
    string? AssetName,
    string? AlertType,
    string? IncidentReports,
    string? DamageReports,
    string? TheftLossRecords,
    string? ResolutionStatus,
    string? UpdatedBy)
{
    public void ApplyTo(AssetIncidentEntity incident)
    {
        incident.AssetId = AssetId ?? string.Empty;
        incident.AssetName = AssetName ?? string.Empty;
        incident.AlertType = AlertType ?? string.Empty;
        incident.IncidentReports = IncidentReports ?? string.Empty;
        incident.DamageReports = DamageReports ?? string.Empty;
        incident.TheftLossRecords = TheftLossRecords ?? string.Empty;
        incident.ResolutionStatus = ResolutionStatus ?? string.Empty;
        incident.UpdatedBy = UpdatedBy;
        incident.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetIncidentResponse(
    string Id,
    string IncidentId,
    string AssetId,
    string AssetName,
    string AlertType,
    string IncidentReports,
    string DamageReports,
    string TheftLossRecords,
    string ResolutionStatus,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetIncidentResponse FromEntity(AssetIncidentEntity incident)
    {
        return new AssetIncidentResponse(
            incident.Id,
            incident.IncidentId,
            incident.AssetId,
            incident.AssetName,
            incident.AlertType,
            incident.IncidentReports,
            incident.DamageReports,
            incident.TheftLossRecords,
            incident.ResolutionStatus,
            incident.CreatedBy,
            incident.CreatedAt,
            incident.UpdatedBy,
            incident.UpdatedAt,
            incident.ClientId,
            incident.TenantId,
            incident.IsDeleted);
    }
}
