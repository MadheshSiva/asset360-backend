using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class IssueTypeMasterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateIssueTypeMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.IssueTypeName))
        {
            errors["IssueTypeName"] = ["IssueTypeName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateIssueTypeMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.IssueTypeName))
        {
            errors["IssueTypeName"] = ["IssueTypeName is required"];
        }

        return errors;
    }
}
