using P360.Project.Api.Contracts;
using P360.Project.Api.Validation;
using P360.Project.Repository.Repositories;
using P360.Repository.Repositories;

namespace P360.Project.Api.Endpoints;

public static class CountryEndpoints
{
    public static RouteGroupBuilder MapCountryEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/countries").WithTags("Countries");

        group.MapGet("", GetCountriesAsync).WithName("GetCountries");
        group.MapGet("/{id}", GetCountryByIdAsync).WithName("GetCountryById");
        group.MapPost("", CreateCountryAsync).WithName("CreateCountry");
        group.MapPut("/{id}", UpdateCountryAsync).WithName("UpdateCountry");
        group.MapDelete("/{id}", DeleteCountryAsync).WithName("DeleteCountry");

        return group;
    }

    private static async Task<IResult> GetCountriesAsync(
        ICountryRepository repository,
        CancellationToken cancellationToken)
    {
        var countries = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(countries.Select(CountryResponse.FromEntity));
    }

    private static async Task<IResult> GetCountryByIdAsync(
        string id,
        ICountryRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid country id." });
        }

        var country = await repository.GetByIdAsync(id, cancellationToken);
        return country is null ? Results.NotFound() : Results.Ok(CountryResponse.FromEntity(country));
    }

    private static async Task<IResult> CreateCountryAsync(
        CreateCountryRequest request,
        ICountryRepository repository,
        IProjectRepository projectRepository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var relationshipErrors = await request.ValidateRelationshipsAsync(projectRepository, cancellationToken);
        if (relationshipErrors.Count > 0)
        {
            return Results.ValidationProblem(relationshipErrors);
        }

        var country = await repository.CreateAsync(request.ToEntity(), cancellationToken);
        return Results.Created($"/api/countries/{country.Id}", CountryResponse.FromEntity(country));
    }

    private static async Task<IResult> UpdateCountryAsync(
        string id,
        UpdateCountryRequest request,
        ICountryRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid country id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var country = await repository.GetByIdAsync(id, cancellationToken);
        if (country is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(country);

        var updated = await repository.UpdateAsync(id, country, cancellationToken);
        return updated ? Results.Ok(CountryResponse.FromEntity(country)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteCountryAsync(
        string id,
        ICountryRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid country id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
