using P360.Domain.Entities;
using P360.Project.Api.Contracts;
using P360.Project.Repository.Repositories;
using P360.Repository.Repositories;

namespace P360.Project.Api.Validation;

internal static class HierarchyRelationshipValidator
{
    public static async Task<IDictionary<string, string[]>> ValidateRelationshipsAsync(
        this CreateCountryRequest request,
        IProjectRepository projectRepository,
        CancellationToken cancellationToken)
    {
        var errors = new ValidationErrorBuilder();

        await RequireExistsAsync(errors, nameof(request.ProjectId), request.ProjectId, "Project", projectRepository, cancellationToken);

        return errors.ToDictionary();
    }

    public static async Task<IDictionary<string, string[]>> ValidateRelationshipsAsync(
        this CreateAreaRequest request,
        IProjectRepository projectRepository,
        ICountryRepository countryRepository,
        CancellationToken cancellationToken)
    {
        var errors = new ValidationErrorBuilder();

        await RequireExistsAsync(errors, nameof(request.ProjectId), request.ProjectId, "Project", projectRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.CountryId), request.CountryId, "Country", countryRepository, cancellationToken);

        return errors.ToDictionary();
    }

    public static async Task<IDictionary<string, string[]>> ValidateRelationshipsAsync(
        this CreateBuildingRequest request,
        IProjectRepository projectRepository,
        ICountryRepository countryRepository,
        IAreaRepository areaRepository,
        CancellationToken cancellationToken)
    {
        var errors = new ValidationErrorBuilder();

        await RequireExistsAsync(errors, nameof(request.ProjectId), request.ProjectId, "Project", projectRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.CountryId), request.CountryId, "Country", countryRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.AreaId), request.AreaId, "Area", areaRepository, cancellationToken);

        return errors.ToDictionary();
    }

    public static async Task<IDictionary<string, string[]>> ValidateRelationshipsAsync(
        this CreateFloorRequest request,
        IProjectRepository projectRepository,
        ICountryRepository countryRepository,
        IAreaRepository areaRepository,
        IBuildingRepository buildingRepository,
        CancellationToken cancellationToken)
    {
        var errors = new ValidationErrorBuilder();

        await RequireExistsAsync(errors, nameof(request.ProjectId), request.ProjectId, "Project", projectRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.CountryId), request.CountryId, "Country", countryRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.AreaId), request.AreaId, "Area", areaRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.BuildingId), request.BuildingId, "Building", buildingRepository, cancellationToken);

        return errors.ToDictionary();
    }

    public static async Task<IDictionary<string, string[]>> ValidateRelationshipsAsync(
        this CreateZoneRequest request,
        IProjectRepository projectRepository,
        ICountryRepository countryRepository,
        IAreaRepository areaRepository,
        IBuildingRepository buildingRepository,
        IFloorRepository floorRepository,
        CancellationToken cancellationToken)
    {
        var errors = new ValidationErrorBuilder();

        await RequireExistsAsync(errors, nameof(request.ProjectId), request.ProjectId, "Project", projectRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.CountryId), request.CountryId, "Country", countryRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.AreaId), request.AreaId, "Area", areaRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.BuildingId), request.BuildingId, "Building", buildingRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.FloorId), request.FloorId, "Floor", floorRepository, cancellationToken);

        return errors.ToDictionary();
    }

    public static async Task<IDictionary<string, string[]>> ValidateRelationshipsAsync(
        this CreateSubZoneRequest request,
        IProjectRepository projectRepository,
        ICountryRepository countryRepository,
        IAreaRepository areaRepository,
        IBuildingRepository buildingRepository,
        IFloorRepository floorRepository,
        IZoneRepository zoneRepository,
        CancellationToken cancellationToken)
    {
        var errors = new ValidationErrorBuilder();

        await RequireExistsAsync(errors, nameof(request.ProjectId), request.ProjectId, "Project", projectRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.CountryId), request.CountryId, "Country", countryRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.AreaId), request.AreaId, "Area", areaRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.BuildingId), request.BuildingId, "Building", buildingRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.FloorId), request.FloorId, "Floor", floorRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.ZoneId), request.ZoneId, "Zone", zoneRepository, cancellationToken);

        return errors.ToDictionary();
    }

    public static async Task<IDictionary<string, string[]>> ValidateRelationshipsAsync(
        this CreateZoneMappingRequest request,
        IProjectRepository projectRepository,
        ICountryRepository countryRepository,
        IAreaRepository areaRepository,
        IBuildingRepository buildingRepository,
        IFloorRepository floorRepository,
        IZoneRepository zoneRepository,
        CancellationToken cancellationToken)
    {
        var errors = new ValidationErrorBuilder();

        await RequireExistsAsync(errors, nameof(request.ProjectId), request.ProjectId, "Project", projectRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.CountryId), request.CountryId, "Country", countryRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.AreaId), request.AreaId, "Area", areaRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.BuildingId), request.BuildingId, "Building", buildingRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.FloorId), request.FloorId, "Floor", floorRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.ZoneId), request.ZoneId, "Zone", zoneRepository, cancellationToken);

        return errors.ToDictionary();
    }

    public static async Task<IDictionary<string, string[]>> ValidateRelationshipsAsync(
        this CreateDeviceZoneMappingRequest request,
        IProjectRepository projectRepository,
        ICountryRepository countryRepository,
        IAreaRepository areaRepository,
        IBuildingRepository buildingRepository,
        IFloorRepository floorRepository,
        IZoneRepository zoneRepository,
        CancellationToken cancellationToken)
    {
        var errors = new ValidationErrorBuilder();

        await RequireExistsAsync(errors, nameof(request.ProjectId), request.ProjectId, "Project", projectRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.CountryId), request.CountryId, "Country", countryRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.AreaId), request.AreaId, "Area", areaRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.BuildingId), request.BuildingId, "Building", buildingRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.FloorId), request.FloorId, "Floor", floorRepository, cancellationToken);
        await RequireExistsAsync(errors, nameof(request.ZoneId), request.ZoneId, "Zone", zoneRepository, cancellationToken);

        return errors.ToDictionary();
    }

    private static async Task RequireExistsAsync<TEntity>(
        ValidationErrorBuilder errors,
        string fieldName,
        string? id,
        string displayName,
        IMongoRepository<TEntity> repository,
        CancellationToken cancellationToken)
        where TEntity : BaseEntity
    {
        if (!MongoObjectId.IsValid(id))
        {
            return;
        }

        var entity = await repository.GetByIdAsync(id!, cancellationToken);
        if (entity is null)
        {
            errors.Error(fieldName, $"{displayName} was not found.");
        }
    }
}
