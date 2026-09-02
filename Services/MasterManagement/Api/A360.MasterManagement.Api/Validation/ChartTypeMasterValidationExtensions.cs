using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class ChartTypeMasterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateChartTypeMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.WidgetName))
        {
            errors["WidgetName"] = ["WidgetName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateChartTypeMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.WidgetName))
        {
            errors["WidgetName"] = ["WidgetName is required"];
        }

        return errors;
    }
}
