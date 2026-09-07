using MongoDB.Bson.Serialization.Attributes;

namespace A360.Domain.Entities;

public sealed class EventLog : BaseEntity
{
    [BsonElement("service_name")]
    public string ServiceName { get; set; } = string.Empty;

    [BsonElement("entity_type")]
    public string EntityType { get; set; } = string.Empty;

    [BsonElement("entity_id")]
    public string EntityId { get; set; } = string.Empty;

    [BsonElement("entity_name")]
    public string EntityName { get; set; } = string.Empty;

    [BsonElement("action")]
    public string Action { get; set; } = string.Empty;
}

public static class EventAction
{
    public const string Created = "Created";
    public const string Updated = "Updated";
    public const string Deleted = "Deleted";
    public const string Viewed = "Viewed";
}
