using PanelSettingEntity = A360.VisitorManagement.Domain.Entities.VisitorPanelSetting;

namespace A360.VisitorManagement.Api.Contracts;

public sealed record CreateVisitorPanelSettingRequest(
    string? BackgroundImg,
    string? Logo,
    string? CompanyName,
    string? ClientId,
    string? CreatedBy,
    bool IsAuthCode,
    bool IsApproved,
    string? VisitorPanelName,
    string? TenantId)
{
    public PanelSettingEntity ToEntity()
    {
        return new PanelSettingEntity
        {
            BackgroundImg = BackgroundImg,
            Logo = Logo,
            CompanyName = CompanyName,
            ClientId = ClientId,
            CreatedBy = CreatedBy,
            IsAuthCode = IsAuthCode,
            IsApproved = IsApproved,
            VisitorPanelName = VisitorPanelName,
            CreatedAt = DateTime.UtcNow,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateVisitorPanelSettingRequest(
    string? BackgroundImg,
    string? Logo,
    string? CompanyName,
    bool IsAuthCode,
    bool IsApproved,
    string? VisitorPanelName,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(PanelSettingEntity setting)
    {
        setting.BackgroundImg = BackgroundImg;
        setting.Logo = Logo;
        setting.CompanyName = CompanyName;
        setting.IsAuthCode = IsAuthCode;
        setting.IsApproved = IsApproved;
        setting.VisitorPanelName = VisitorPanelName;
        setting.UpdatedBy = UpdatedBy;
        setting.UpdatedAt = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(Status))
        {
            setting.Status = Status;
        }
    }
}

public sealed record VisitorPanelSettingResponse(
    string Id,
    string BackgroundImg,
    string Logo,
    string CompanyName,
    string ClientId,
    string CreatedBy,
    DateTime? CreatedAt,
    bool IsAuthCode,
    bool IsApproved,
    string VisitorPanelName,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? TenantId,
    bool IsDeleted,
    string? Status)
{
    public static VisitorPanelSettingResponse FromEntity(
        PanelSettingEntity setting)
    {
        return new VisitorPanelSettingResponse(
            setting.Id ?? string.Empty,
            setting.BackgroundImg ?? string.Empty,
            setting.Logo ?? string.Empty,
            setting.CompanyName ?? string.Empty,
            setting.ClientId ?? string.Empty,
            setting.CreatedBy ?? string.Empty,
            setting.CreatedAt,
            setting.IsAuthCode,
            setting.IsApproved,
            setting.VisitorPanelName ?? string.Empty,
            setting.UpdatedBy,
            setting.UpdatedAt,
            setting.TenantId,
            setting.IsDeleted,
            setting.Status);
    }
}
