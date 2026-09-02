using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class UpdateSourceMasterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateUpdateSourceMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SourceName))
        {
            errors["SourceName"] = ["SourceName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateUpdateSourceMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SourceName))
        {
            errors["SourceName"] = ["SourceName is required"];
        }

        return errors;
    }
}
