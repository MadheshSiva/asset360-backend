using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.VisitorManagement.Domain.Entities;

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
}
