using AssetCheckinEntity = A360.Asset.Domain.Entities.AssetCheckin;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetCheckinRequest(
    string? AssetId,
    string? AssetName,
    string? AssetCode,
    string? AssetDescription,
    string? Company,
    string? Site,
    string? Building,
    string? Floor,
    string? Room,
    string? DepartmentName,
    string? CustodianName,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetCheckinEntity ToEntity(string checkinId)
    {
        return new AssetCheckinEntity
        {
            CheckinId = checkinId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            AssetCode = AssetCode ?? string.Empty,
            AssetDescription = AssetDescription ?? string.Empty,
            Company = Company ?? string.Empty,
            Site = Site ?? string.Empty,
            Building = Building ?? string.Empty,
            Floor = Floor ?? string.Empty,
            Room = Room ?? string.Empty,
            DepartmentName = DepartmentName ?? string.Empty,
            CustodianName = CustodianName ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetCheckinRequest(
    string? AssetId,
    string? AssetName,
    string? AssetCode,
    string? AssetDescription,
    string? Company,
    string? Site,
    string? Building,
    string? Floor,
    string? Room,
    string? DepartmentName,
    string? CustodianName,
    string? UpdatedBy)
{
    public void ApplyTo(AssetCheckinEntity checkin)
    {
        checkin.AssetId = AssetId ?? string.Empty;
        checkin.AssetName = AssetName ?? string.Empty;
        checkin.AssetCode = AssetCode ?? string.Empty;
        checkin.AssetDescription = AssetDescription ?? string.Empty;
        checkin.Company = Company ?? string.Empty;
        checkin.Site = Site ?? string.Empty;
        checkin.Building = Building ?? string.Empty;
        checkin.Floor = Floor ?? string.Empty;
        checkin.Room = Room ?? string.Empty;
        checkin.DepartmentName = DepartmentName ?? string.Empty;
        checkin.CustodianName = CustodianName ?? string.Empty;
        checkin.UpdatedBy = UpdatedBy;
        checkin.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetCheckinResponse(
    string Id,
    string CheckinId,
    string AssetId,
    string AssetName,
    string AssetCode,
    string AssetDescription,
    string Company,
    string Site,
    string Building,
    string Floor,
    string Room,
    string DepartmentName,
    string CustodianName,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetCheckinResponse FromEntity(AssetCheckinEntity checkin)
    {
        return new AssetCheckinResponse(
            checkin.Id,
            checkin.CheckinId,
            checkin.AssetId,
            checkin.AssetName,
            checkin.AssetCode,
            checkin.AssetDescription,
            checkin.Company,
            checkin.Site,
            checkin.Building,
            checkin.Floor,
            checkin.Room,
            checkin.DepartmentName,
            checkin.CustodianName,
            checkin.CreatedBy,
            checkin.CreatedAt,
            checkin.UpdatedBy,
            checkin.UpdatedAt,
            checkin.ClientId,
            checkin.TenantId,
            checkin.IsDeleted);
    }
}
