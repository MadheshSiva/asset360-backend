using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class PhysicalVerificationResultValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreatePhysicalVerificationResultRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ResultName))
        {
            errors["ResultName"] = ["ResultName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ResultCode))
        {
            errors["ResultCode"] = ["ResultCode is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdatePhysicalVerificationResultRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ResultName))
        {
            errors["ResultName"] = ["ResultName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ResultCode))
        {
            errors["ResultCode"] = ["ResultCode is required"];
        }

        return errors;
    }
}
