using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class ResourceTypeValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateResourceTypeRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.TypeName))
        {
            errors["TypeName"] = ["TypeName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateResourceTypeRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.TypeName))
        {
            errors["TypeName"] = ["TypeName is required"];
        }

        return errors;
    }
}
