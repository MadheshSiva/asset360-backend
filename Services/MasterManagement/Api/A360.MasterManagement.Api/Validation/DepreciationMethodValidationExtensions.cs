using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class DepreciationMethodValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateDepreciationMethodRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.MethodName))
        {
            errors["MethodName"] = ["MethodName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.MethodCode))
        {
            errors["MethodCode"] = ["MethodCode is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateDepreciationMethodRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.MethodName))
        {
            errors["MethodName"] = ["MethodName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.MethodCode))
        {
            errors["MethodCode"] = ["MethodCode is required"];
        }

        return errors;
    }
}
