using MongoDB.Bson.Serialization.Attributes;
using P360.Domain.Entities;

namespace P360.VisitorManagement.Domain.Entities;

public class VisitorIdentification : BaseEntity
{
    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("identification_type")]
    public string IdentificationType { get; set; } = null!;

    [BsonElement("reader_id")]
    public string? ReaderId { get; set; }

    [BsonElement("entryexist_id")]
    public string? EntryExistId { get; set; }

    [BsonElement("entryexist_point")]
    public string? EntryExistPoint { get; set; }

    [BsonElement("readertype_id")]
    public string? ReaderTypeId { get; set; }

    [BsonElement("readertype_name")]
    public string? ReaderTypeName { get; set; }

    [BsonElement("created_by")]
    public string? CreatedBy { get; set; }

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }
}
