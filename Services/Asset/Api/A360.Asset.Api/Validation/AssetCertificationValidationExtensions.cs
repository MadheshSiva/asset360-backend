using A360.Asset.Api.Contracts;

namespace A360.Asset.Api.Validation;

public static class AssetCertificationValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateAssetCertificationRequest request)
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

        if (string.IsNullOrWhiteSpace(request.CertificationType))
        {
            errors["CertificationType"] = ["CertificationType is required"];
        }

        if (request.ExpiryDate.HasValue && request.IssuedDate.HasValue
            && request.ExpiryDate < request.IssuedDate)
        {
            errors["ExpiryDate"] = ["ExpiryDate cannot be earlier than IssuedDate"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAssetCertificationRequest request)
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

        if (string.IsNullOrWhiteSpace(request.CertificationType))
        {
            errors["CertificationType"] = ["CertificationType is required"];
        }

        if (request.ExpiryDate.HasValue && request.IssuedDate.HasValue
            && request.ExpiryDate < request.IssuedDate)
        {
            errors["ExpiryDate"] = ["ExpiryDate cannot be earlier than IssuedDate"];
        }

        return errors;
    }
}
