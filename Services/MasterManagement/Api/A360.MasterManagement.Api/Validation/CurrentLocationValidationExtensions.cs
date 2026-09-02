using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class CurrentLocationValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateCurrentLocationRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.CurrentLocationName))
        {
            errors["CurrentLocationName"] = ["CurrentLocationName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateCurrentLocationRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.CurrentLocationName))
        {
            errors["CurrentLocationName"] = ["CurrentLocationName is required"];
        }

        return errors;
    }
}
