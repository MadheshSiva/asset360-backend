
using P360.Devices.Api.Contracts;
using P360.Devices.Api.Validation;
using P360.Devices.Repository.Repositories;
using P360.Repository.Repositories;

namespace P360.Devices.Api.Endpoints;

public static class DeviceEndpoints
{
    public static RouteGroupBuilder MapDeviceEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/devices")
            .WithTags("Devices");

        group.MapGet("", GetDevicesAsync)
            .WithName("GetDevices");

        group.MapGet("/{id}", GetDeviceByIdAsync)
            .WithName("GetDeviceById");

        group.MapGet("/type/{type}", GetDevicesByTypeAsync)
            .WithName("GetDevicesByType");

        group.MapPost("", CreateDeviceAsync)
            .WithName("CreateDevice");

        group.MapPut("/{id}", UpdateDeviceAsync)
            .WithName("UpdateDevice");

        group.MapDelete("/{id}", DeleteDeviceAsync)
            .WithName("DeleteDevice");

        return group;
    }

    private static async Task<IResult> GetDevicesAsync(
        IDeviceRepository repository,
        CancellationToken cancellationToken)
    {
        var devices = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            devices.Select(DeviceResponse.FromEntity));
    }

    private static async Task<IResult> GetDeviceByIdAsync(
        string id,
        IDeviceRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid device id." });
        }

        var device = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return device is null
            ? Results.NotFound()
            : Results.Ok(DeviceResponse.FromEntity(device));
    }

    private static async Task<IResult> GetDevicesByTypeAsync(
        string type,
        IDeviceRepository repository,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            return Results.BadRequest(
                new { message = "Device type is required." });
        }

        var devices = await repository.GetByTypeAsync(
            type,
            cancellationToken);

        return Results.Ok(
            devices.Select(DeviceResponse.FromEntity));
    }

    private static async Task<IResult> CreateDeviceAsync(
        CreateDeviceRequest request,
        IDeviceRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var device = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/devices/{device.Id}",
            DeviceResponse.FromEntity(device));
    }

    private static async Task<IResult> UpdateDeviceAsync(
        string id,
        UpdateDeviceRequest request,
        IDeviceRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid device id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var device = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (device is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(device);

        var updated = await repository.UpdateAsync(
            id,
            device,
            cancellationToken);

        return updated
            ? Results.Ok(DeviceResponse.FromEntity(device))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteDeviceAsync(
        string id,
        IDeviceRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid device id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}
