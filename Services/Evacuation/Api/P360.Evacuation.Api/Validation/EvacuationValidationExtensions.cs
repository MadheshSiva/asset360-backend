
using P360.Evacuation.Api.Contracts;

namespace P360.Evacuation.Api.Validation;

public static class EvacuationValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateEvacuationRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.ProjectId))
        {
            errors["ProjectId"] =
                ["ProjectId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ProjectName))
        {
            errors["ProjectName"] =
                ["ProjectName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.BuildingId))
        {
            errors["BuildingId"] =
                ["BuildingId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.BuildingName))
        {
            errors["BuildingName"] =
                ["BuildingName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateEvacuationRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.ProjectId))
        {
            errors["ProjectId"] =
                ["ProjectId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ProjectName))
        {
            errors["ProjectName"] =
                ["ProjectName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.BuildingId))
        {
            errors["BuildingId"] =
                ["BuildingId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.BuildingName))
        {
            errors["BuildingName"] =
                ["BuildingName is required"];
        }

        return errors;
    }
}
