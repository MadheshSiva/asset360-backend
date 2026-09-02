using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class TagValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateTagRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.TagCode))
        {
            errors["TagCode"] = ["TagCode is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateTagRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.TagCode))
        {
            errors["TagCode"] = ["TagCode is required"];
        }

        return errors;
    }
}
