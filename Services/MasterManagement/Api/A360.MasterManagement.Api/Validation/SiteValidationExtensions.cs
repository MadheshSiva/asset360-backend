using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class SiteValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateSiteRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SiteName))
        {
            errors["SiteName"] = ["SiteName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Organization))
        {
            errors["Organization"] = ["Organization is required"];
        }

        if (string.IsNullOrWhiteSpace(request.BusinessUnit))
        {
            errors["BusinessUnit"] = ["BusinessUnit is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateSiteRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SiteName))
        {
            errors["SiteName"] = ["SiteName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Organization))
        {
            errors["Organization"] = ["Organization is required"];
        }

        if (string.IsNullOrWhiteSpace(request.BusinessUnit))
        {
            errors["BusinessUnit"] = ["BusinessUnit is required"];
        }

        return errors;
    }
}
