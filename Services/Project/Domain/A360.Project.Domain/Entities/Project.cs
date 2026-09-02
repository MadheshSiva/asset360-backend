using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Project.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class Project : BaseEntity
{
    [BsonElement("project_name")]
    public string ProjectName { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("status")]
    public new bool Status { get; set; }

    [BsonElement("week_start")]
    public DateTime WeekStart { get; set; }

    [BsonElement("week_end")]
    public DateTime WeekEnd { get; set; }
}
