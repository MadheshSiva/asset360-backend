using A360.Asset.Api.Contracts;

namespace A360.Asset.Api.Validation;

public static class AssetAuditValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateAssetAuditRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AssetName))
        {
            errors["AssetName"] = ["AssetName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AuditCode))
        {
            errors["AuditCode"] = ["AuditCode is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AuditName))
        {
            errors["AuditName"] = ["AuditName is required"];
        }

        if (request.AuditEndDate.HasValue && request.AuditStartDate.HasValue
            && request.AuditEndDate < request.AuditStartDate)
        {
            errors["AuditEndDate"] = ["AuditEndDate cannot be earlier than AuditStartDate"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAssetAuditRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AssetName))
        {
            errors["AssetName"] = ["AssetName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AuditCode))
        {
            errors["AuditCode"] = ["AuditCode is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AuditName))
        {
            errors["AuditName"] = ["AuditName is required"];
        }

        if (request.AuditEndDate.HasValue && request.AuditStartDate.HasValue
            && request.AuditEndDate < request.AuditStartDate)
        {
            errors["AuditEndDate"] = ["AuditEndDate cannot be earlier than AuditStartDate"];
        }

        return errors;
    }
}
