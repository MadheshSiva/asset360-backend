using PatientMasterEntity =
    P360.OTManagement.Domain.Entities.PatientMaster;

using MongoDB.Driver;
using P360.Repository.Repositories;

namespace P360.OTManagement.Repository.Repositories;

public sealed class PatientMasterRepository
    : MongoRepository<PatientMasterEntity>,
      IPatientMasterRepository,
      IMongoIndexConfigurator
{
    public const string CollectionName =
        "patientmaster";

    public PatientMasterRepository(
        IMongoDatabase database)
        : base(
            database.GetCollection<PatientMasterEntity>(
                CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<PatientMasterEntity>(
                Builders<PatientMasterEntity>.IndexKeys
                    .Ascending(x => x.HisId),
                new CreateIndexOptions
                {
                    Name = "ix_patientmaster_hisid",
                    Unique = true
                }),

            new CreateIndexModel<PatientMasterEntity>(
                Builders<PatientMasterEntity>.IndexKeys
                    .Ascending(x => x.CaseId),
                new CreateIndexOptions
                {
                    Name = "ix_patientmaster_caseid",
                    Unique = true
                }),

            new CreateIndexModel<PatientMasterEntity>(
                Builders<PatientMasterEntity>.IndexKeys
                    .Ascending(x => x.PatientName),
                new CreateIndexOptions
                {
                    Name = "ix_patientmaster_name"
                }),

            new CreateIndexModel<PatientMasterEntity>(
                Builders<PatientMasterEntity>.IndexKeys
                    .Ascending(x => x.Department)
                    .Ascending(x => x.Priority),
                new CreateIndexOptions
                {
                    Name = "ix_patientmaster_department_priority"
                }),

            new CreateIndexModel<PatientMasterEntity>(
                Builders<PatientMasterEntity>.IndexKeys
                    .Ascending(x => x.SurgeryType),
                new CreateIndexOptions
                {
                    Name = "ix_patientmaster_surgerytype"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}