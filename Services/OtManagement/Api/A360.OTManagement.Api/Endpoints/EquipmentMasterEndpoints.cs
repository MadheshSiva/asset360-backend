using A360.OTManagement.Api.Contracts;
using A360.OTManagement.Api.Validation;
using A360.OTManagement.Repository.Repositories;
using A360.Repository.Repositories;

namespace A360.OTManagement.Api.Endpoints;

public static class EquipmentMasterEndpoints
{
    public static RouteGroupBuilder MapEquipmentMasterEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/equipmentmaster")
            .WithTags("EquipmentMaster");

        group.MapGet("", GetEquipmentMastersAsync)
            .WithName("GetEquipmentMasters");

        group.MapGet("/{id}", GetEquipmentMasterByIdAsync)
            .WithName("GetEquipmentMasterById");

        group.MapPost("", CreateEquipmentMasterAsync)
            .WithName("CreateEquipmentMaster");

        group.MapPut("/{id}", UpdateEquipmentMasterAsync)
            .WithName("UpdateEquipmentMaster");

        group.MapDelete("/{id}", DeleteEquipmentMasterAsync)
            .WithName("DeleteEquipmentMaster");

        return group;
    }

    private static async Task<IResult> GetEquipmentMastersAsync(
        IEquipmentMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var equipmentMasters = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            equipmentMasters.Select(
                EquipmentMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetEquipmentMasterByIdAsync(
        string id,
        IEquipmentMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid equipment master id." });
        }

        var equipmentMaster = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return equipmentMaster is null
            ? Results.NotFound()
            : Results.Ok(
                EquipmentMasterResponse.FromEntity(
                    equipmentMaster));
    }

    private static async Task<IResult> CreateEquipmentMasterAsync(
        CreateEquipmentMasterRequest request,
        IEquipmentMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(
                validationErrors);
        }

        var equipmentMaster = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/equipmentmaster/{equipmentMaster.Id}",
            EquipmentMasterResponse.FromEntity(
                equipmentMaster));
    }

    private static async Task<IResult> UpdateEquipmentMasterAsync(
        string id,
        UpdateEquipmentMasterRequest request,
        IEquipmentMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid equipment master id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(
                validationErrors);
        }

        var equipmentMaster = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (equipmentMaster is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(equipmentMaster);

        var updated = await repository.UpdateAsync(
            id,
            equipmentMaster,
            cancellationToken);

        return updated
            ? Results.Ok(
                EquipmentMasterResponse.FromEntity(
                    equipmentMaster))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteEquipmentMasterAsync(
        string id,
        IEquipmentMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid equipment master id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}
