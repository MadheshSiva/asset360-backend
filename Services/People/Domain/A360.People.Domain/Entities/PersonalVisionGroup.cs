using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.People.Domain.Entities;

public class PersonalVisionGroup : BaseEntity
{
    [BsonElement("userid")]
    public string UserId { get; set; } = null!;

    [BsonElement("group_type")]
    public string GroupType { get; set; } = null!;

    [BsonElement("group_name")]
    public string GroupName { get; set; } = null!;

    [BsonElement("Members")]
    public List<GroupMember> Members { get; set; } = [];

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