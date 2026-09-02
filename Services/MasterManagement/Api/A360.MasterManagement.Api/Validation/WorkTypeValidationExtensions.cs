using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class WorkTypeValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateWorkTypeRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.WorkTypeName))
        {
            errors["WorkTypeName"] = ["WorkTypeName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateWorkTypeRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.WorkTypeName))
        {
            errors["WorkTypeName"] = ["WorkTypeName is required"];
        }

        return errors;
    }
}
