using A360.OTManagement.Api.Contracts;
using A360.OTManagement.Api.Validation;
using A360.OTManagement.Repository.Repositories;
using A360.Repository.Repositories;

namespace A360.OTManagement.Api.Endpoints;

public static class PatientMasterEndpoints
{
    public static RouteGroupBuilder MapPatientMasterEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/patientmaster")
            .WithTags("PatientMaster");

        group.MapGet("", GetPatientMastersAsync)
            .WithName("GetPatientMasters");

        group.MapGet("/{id}", GetPatientMasterByIdAsync)
            .WithName("GetPatientMasterById");

        group.MapPost("", CreatePatientMasterAsync)
            .WithName("CreatePatientMaster");

        group.MapPut("/{id}", UpdatePatientMasterAsync)
            .WithName("UpdatePatientMaster");

        group.MapDelete("/{id}", DeletePatientMasterAsync)
            .WithName("DeletePatientMaster");

        return group;
    }

    private static async Task<IResult> GetPatientMastersAsync(
        IPatientMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var patientMasters = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            patientMasters.Select(
                PatientMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetPatientMasterByIdAsync(
        string id,
        IPatientMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid Patient Master id." });
        }

        var patientMaster = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return patientMaster is null
            ? Results.NotFound()
            : Results.Ok(
                PatientMasterResponse.FromEntity(
                    patientMaster));
    }

    private static async Task<IResult> CreatePatientMasterAsync(
        CreatePatientMasterRequest request,
        IPatientMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(
                validationErrors);
        }

        var patientMaster = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/patientmaster/{patientMaster.Id}",
            PatientMasterResponse.FromEntity(
                patientMaster));
    }

    private static async Task<IResult> UpdatePatientMasterAsync(
        string id,
        UpdatePatientMasterRequest request,
        IPatientMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid Patient Master id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(
                validationErrors);
        }

        var patientMaster = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (patientMaster is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(patientMaster);

        var updated = await repository.UpdateAsync(
            id,
            patientMaster,
            cancellationToken);

        return updated
            ? Results.Ok(
                PatientMasterResponse.FromEntity(
                    patientMaster))
            : Results.NotFound();
    }

    private static async Task<IResult> DeletePatientMasterAsync(
        string id,
        IPatientMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid Patient Master id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}