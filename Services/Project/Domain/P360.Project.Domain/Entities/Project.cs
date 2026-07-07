using MongoDB.Bson.Serialization.Attributes;
using P360.Domain.Entities;

namespace P360.Project.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class Project : BaseEntity
{
    [BsonElement("project_name")]
    public string ProjectName { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("status")]
    public bool Status { get; set; }

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = string.Empty;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("client_id")]
    public string ClientId { get; set; } = string.Empty;

    [BsonElement("week_start")]
    public DateTime WeekStart { get; set; }

    [BsonElement("week_end")]
    public DateTime WeekEnd { get; set; }
}
