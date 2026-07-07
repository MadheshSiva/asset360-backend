using A360.People.Api.Contracts;

namespace A360.People.Api.Validation;

public static class PersonalVisionGreetingsIndividualValidationExtensions
{
public static Dictionary<string, string[]> Validate(
this CreatePersonalVisionGreetingsIndividualRequest request)
{
var errors = new Dictionary<string, string[]>();


    if (request.MemberList is null || !request.MemberList.Any())
    {
        errors["MemberList"] =
            ["At least one member is required"];
    }

    if (string.IsNullOrWhiteSpace(request.MemberType))
    {
        errors["MemberType"] =
            ["MemberType is required"];
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
    this UpdatePersonalVisionGreetingsIndividualRequest request)
{
    var errors = new Dictionary<string, string[]>();

    if (request.MemberList is null || !request.MemberList.Any())
    {
        errors["MemberList"] =
            ["At least one member is required"];
    }

    if (string.IsNullOrWhiteSpace(request.MemberType))
    {
        errors["MemberType"] =
            ["MemberType is required"];
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
