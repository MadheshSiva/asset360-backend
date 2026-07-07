using A360.People.Api.Contracts;

namespace A360.People.Api.Validation;

public static class ContractorValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateContractorRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.ContractorName))
        {
            errors["ContractorName"] =
                ["ContractorName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ContractorId))
        {
            errors["ContractorId"] =
                ["ContractorId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.CompanyName))
        {
            errors["CompanyName"] =
                ["CompanyName is required"];
        }

        if (request.ContractStart == default)
        {
            errors["ContractStart"] =
                ["ContractStart is required"];
        }

        if (request.ContractEnd == default)
        {
            errors["ContractEnd"] =
                ["ContractEnd is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateContractorRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.ContractorName))
        {
            errors["ContractorName"] =
                ["ContractorName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ContractorId))
        {
            errors["ContractorId"] =
                ["ContractorId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.CompanyName))
        {
            errors["CompanyName"] =
                ["CompanyName is required"];
        }

        if (request.ContractStart == default)
        {
            errors["ContractStart"] =
                ["ContractStart is required"];
        }

        if (request.ContractEnd == default)
        {
            errors["ContractEnd"] =
                ["ContractEnd is required"];
        }

        return errors;
    }
}