using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class SkillMasterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateSkillMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SkillName))
        {
            errors["SkillName"] = ["SkillName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateSkillMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SkillName))
        {
            errors["SkillName"] = ["SkillName is required"];
        }

        return errors;
    }
}
