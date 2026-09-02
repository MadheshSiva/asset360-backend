using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class ResponseTypeMasterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateResponseTypeMasterRequest request)
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

    public static Dictionary<string, string[]> Validate(this UpdateResponseTypeMasterRequest request)
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
