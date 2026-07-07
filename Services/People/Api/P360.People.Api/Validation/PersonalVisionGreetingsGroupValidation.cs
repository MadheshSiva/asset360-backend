using P360.People.Api.Contracts;

namespace P360.People.Api.Validation;

public static class PersonalVisionGreetingsGroupsValidationExtensions
{
public static Dictionary<string, string[]> Validate(
this CreatePersonalVisionGreetingsGroupsRequest request)
{
var errors = new Dictionary<string, string[]>();


    if (request.Members is null || !request.Members.Any())
    {
        errors["Members"] =
            ["At least one member is required"];
    }

    if (string.IsNullOrWhiteSpace(request.GroupType))
    {
        errors["GroupType"] =
            ["GroupType is required"];
    }

    if (string.IsNullOrWhiteSpace(request.GroupName))
    {
        errors["GroupName"] =
            ["GroupName is required"];
    }

    if (string.IsNullOrWhiteSpace(request.GreetingsType))
    {
        errors["GreetingsType"] =
            ["GreetingsType is required"];
    }

    if (string.IsNullOrWhiteSpace(request.GreetingsDescription))
    {
        errors["GreetingsDescription"] =
            ["GreetingsDescription is required"];
    }

    return errors;
}

public static Dictionary<string, string[]> Validate(
    this UpdatePersonalVisionGreetingsGroupsRequest request)
{
    var errors = new Dictionary<string, string[]>();

    if (request.Members is null || !request.Members.Any())
    {
        errors["Members"] =
            ["At least one member is required"];
    }

    if (string.IsNullOrWhiteSpace(request.GroupType))
    {
        errors["GroupType"] =
            ["GroupType is required"];
    }

    if (string.IsNullOrWhiteSpace(request.GroupName))
    {
        errors["GroupName"] =
            ["GroupName is required"];
    }

    if (string.IsNullOrWhiteSpace(request.GreetingsType))
    {
        errors["GreetingsType"] =
            ["GreetingsType is required"];
    }

    if (string.IsNullOrWhiteSpace(request.GreetingsDescription))
    {
        errors["GreetingsDescription"] =
            ["GreetingsDescription is required"];
    }

    return errors;
}


}
