using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class CertificationTypeMasterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateCertificationTypeMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.CertificationName))
        {
            errors["CertificationName"] = ["CertificationName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.CertificationCode))
        {
            errors["CertificationCode"] = ["CertificationCode is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateCertificationTypeMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.CertificationName))
        {
            errors["CertificationName"] = ["CertificationName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.CertificationCode))
        {
            errors["CertificationCode"] = ["CertificationCode is required"];
        }

        return errors;
    }
}
