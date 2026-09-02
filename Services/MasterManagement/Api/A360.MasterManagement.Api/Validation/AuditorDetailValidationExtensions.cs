using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class AuditorDetailValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateAuditorDetailRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AuditorName))
        {
            errors["AuditorName"] = ["AuditorName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.EmployeeCode))
        {
            errors["EmployeeCode"] = ["EmployeeCode is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAuditorDetailRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AuditorName))
        {
            errors["AuditorName"] = ["AuditorName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.EmployeeCode))
        {
            errors["EmployeeCode"] = ["EmployeeCode is required"];
        }

        return errors;
    }
}
