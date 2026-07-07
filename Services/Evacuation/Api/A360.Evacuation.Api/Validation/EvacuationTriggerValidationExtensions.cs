
using A360.Evacuation.Api.Contracts;

namespace A360.Evacuation.Api.Validation;

public static class EvacuationTriggerValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateEvacuationTriggerRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.TriggerField))
        {
            errors["TriggerField"] =
                ["TriggerField is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateEvacuationTriggerRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.TriggerField))
        {
            errors["TriggerField"] =
                ["TriggerField is required"];
        }

        return errors;
    }
}
