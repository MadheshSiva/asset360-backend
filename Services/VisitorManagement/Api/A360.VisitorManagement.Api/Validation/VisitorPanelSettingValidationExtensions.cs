using A360.VisitorManagement.Api.Contracts;

namespace A360.VisitorManagement.Api.Validation;

public static class VisitorPanelSettingValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateVisitorPanelSettingRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.ClientId))
        {
            errors["ClientId"] =
                ["ClientId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.VisitorPanelName))
        {
            errors["VisitorPanelName"] =
                ["VisitorPanelName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateVisitorPanelSettingRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.VisitorPanelName))
        {
            errors["VisitorPanelName"] =
                ["VisitorPanelName is required"];
        }

        return errors;
    }
}
