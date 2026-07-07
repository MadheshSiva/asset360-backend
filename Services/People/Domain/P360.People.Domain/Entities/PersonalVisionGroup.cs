using MongoDB.Bson.Serialization.Attributes;
using P360.Domain.Entities;

namespace P360.People.Domain.Entities;

public class PersonalVisionGroup : BaseEntity
{
    [BsonElement("clientid")]
    public string ClientId { get; set; } = null!;

    [BsonElement("userid")]
    public string UserId { get; set; } = null!;

    [BsonElement("group_type")]
    public string GroupType { get; set; } = null!;

    [BsonElement("group_name")]
    public string GroupName { get; set; } = null!;

    [BsonElement("Members")]
    public List<GroupMember> Members { get; set; } = [];

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("modified_by")]
    public string ModifiedBy { get; set; } = null!;

    [BsonElement("modified_at")]
    public DateTime ModifiedAt { get; set; }

    [BsonElement("isactive")]
    public bool IsActive { get; set; }
}

public class GroupMember
{
    [BsonElement("MemberID")]
    public string MemberID { get; set; } = null!;

    [BsonElement("MemberName")]
    public string MemberName { get; set; } = null!;
}