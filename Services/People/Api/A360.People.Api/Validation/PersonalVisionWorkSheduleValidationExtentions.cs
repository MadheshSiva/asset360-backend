using A360.People.Api.Contracts;

namespace A360.People.Api.Validation;

public static class PersonalWorkScheduleValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreatePersonalWorkScheduleRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.WorkScheduleName))
        {
            errors["WorkScheduleName"] =
                ["Work Schedule Name is required"];
        }

        if (string.IsNullOrWhiteSpace(request.GroupName))
        {
            errors["GroupName"] =
                ["Group Name is required"];
        }

        if (string.IsNullOrWhiteSpace(request.GroupId))
        {
            errors["GroupId"] =
                ["Group Id is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ScheduleType))
        {
            errors["ScheduleType"] =
                ["Schedule Type is required"];
        }

        if (request.WorkSchedules is null || request.WorkSchedules.Count == 0)
        {
            errors["WorkSchedules"] =
                ["At least one work schedule is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdatePersonalWorkScheduleRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.WorkScheduleName))
        {
            errors["WorkScheduleName"] =
                ["Work Schedule Name is required"];
        }

        if (string.IsNullOrWhiteSpace(request.GroupName))
        {
            errors["GroupName"] =
                ["Group Name is required"];
        }

        if (string.IsNullOrWhiteSpace(request.GroupId))
        {
            errors["GroupId"] =
                ["Group Id is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ScheduleType))
        {
            errors["ScheduleType"] =
                ["Schedule Type is required"];
        }

        if (request.WorkSchedules is null || request.WorkSchedules.Count == 0)
        {
            errors["WorkSchedules"] =
                ["At least one work schedule is required"];
        }

        return errors;
    }
}