using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class CategoryValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateCategoryRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.CategoryName))
        {
            errors["CategoryName"] = ["CategoryName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.CategoryCode))
        {
            errors["CategoryCode"] = ["CategoryCode is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateCategoryRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.CategoryName))
        {
            errors["CategoryName"] = ["CategoryName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.CategoryCode))
        {
            errors["CategoryCode"] = ["CategoryCode is required"];
        }

        return errors;
    }
}
