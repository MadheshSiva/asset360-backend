using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.People.Domain.Entities;

public class PersonalVisionGreetingsGroups : BaseEntity
{
    [BsonElement("members")]
    public List<GreetingsGroupMember> Members { get; set; } = new();

    [BsonElement("group_type")]
    public string GroupType { get; set; } = null!;

    [BsonElement("group_name")]
    public string GroupName { get; set; } = null!;

    [BsonElement("greetings_type")]
    public string GreetingsType { get; set; } = null!;

    [BsonElement("greetings_description")]
    public string GreetingsDescription { get; set; } = null!;

    [BsonElement("status")]
    public bool Status { get; set; }

    [BsonElement("GreetingsTimeSchedules")]
    public List<GreetingsTimeSchedule1> GreetingsTimeSchedules { get; set; } = new();

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("modified_by")]
    public string? ModifiedBy { get; set; }

    [BsonElement("modified_at")]
    public DateTime? ModifiedAt { get; set; }
}

public class GreetingsGroupMember
{
    [BsonElement("member_id")]
    public string MemberId { get; set; } = null!;

    [BsonElement("member_name")]
    public string MemberName { get; set; } = null!;
}

public class GreetingsTimeSchedule1
{
    [BsonElement("from_date")]
    public DateTime FromDate { get; set; }

    [BsonElement("to_date")]
    public DateTime ToDate { get; set; }

    [BsonElement("from_time")]
    public string FromTime { get; set; } = null!;

    [BsonElement("to_time")]
    public string ToTime { get; set; } = null!;
}