using PanelSettingEntity = P360.VisitorManagement.Domain.Entities.VisitorPanelSetting;

namespace P360.VisitorManagement.Api.Contracts;

public sealed record CreateVisitorPanelSettingRequest(
    string? BackgroundImg,
    string? Logo,
    string? CompanyName,
    string? ClientId,
    string? CreatedBy,
    bool IsAuthCode,
    bool IsApproved,
    string? VisitorPanelName)
{
    public PanelSettingEntity ToEntity()
    {
        return new PanelSettingEntity
        {
            BackgroundImg = BackgroundImg,
            Logo = Logo,
            CompanyName = CompanyName,
            ClientId = ClientId!,
            CreatedBy = CreatedBy,
            IsAuthCode = IsAuthCode,
            IsApproved = IsApproved,
            VisitorPanelName = VisitorPanelName,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public sealed record UpdateVisitorPanelSettingRequest(
    string? BackgroundImg,
    string? Logo,
    string? CompanyName,
    bool IsAuthCode,
    bool IsApproved,
    string? VisitorPanelName)
{
    public void ApplyTo(PanelSettingEntity setting)
    {
        setting.BackgroundImg = BackgroundImg;
        setting.Logo = Logo;
        setting.CompanyName = CompanyName;
        setting.IsAuthCode = IsAuthCode;
        setting.IsApproved = IsApproved;
        setting.VisitorPanelName = VisitorPanelName;
    }
}

public sealed record VisitorPanelSettingResponse(
    string Id,
    string BackgroundImg,
    string Logo,
    string CompanyName,
    string ClientId,
    string CreatedBy,
    DateTime CreatedAt,
    bool IsAuthCode,
    bool IsApproved,
    string VisitorPanelName)
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
            setting.VisitorPanelName ?? string.Empty);
    }
}
