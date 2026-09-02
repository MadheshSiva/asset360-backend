using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class AlertTypeValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateAlertTypeRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AlertName))
        {
            errors["AlertName"] = ["AlertName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AlertCode))
        {
            errors["AlertCode"] = ["AlertCode is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAlertTypeRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AlertName))
        {
            errors["AlertName"] = ["AlertName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AlertCode))
        {
            errors["AlertCode"] = ["AlertCode is required"];
        }

        return errors;
    }
}
