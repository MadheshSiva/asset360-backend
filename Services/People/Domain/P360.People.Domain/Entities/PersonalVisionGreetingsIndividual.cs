using MongoDB.Bson.Serialization.Attributes;
using P360.Domain.Entities;

namespace P360.People.Domain.Entities;

public class PersonalVisionGreetingsIndividual : BaseEntity
{
[BsonElement("member_list")]
public List<GreetingsIndividualMember> MemberList { get; set; } = new();

[BsonElement("member_type")]
public string MemberType { get; set; } = null!;

[BsonElement("greetings_type")]
public string GreetingsType { get; set; } = null!;

[BsonElement("greetings_description")]
public string GreetingsDescription { get; set; } = null!;

[BsonElement("status")]
public bool Status { get; set; }

[BsonElement("greetings_time_schedules")]
public List<GreetingsTimeSchedule> GreetingsTimeSchedules { get; set; } = new();

[BsonElement("created_by")]
public string CreatedBy { get; set; } = null!;

[BsonElement("created_at")]
public DateTime CreatedAt { get; set; }

[BsonElement("modified_at")]
public DateTime? ModifiedAt { get; set; }


}

public class GreetingsIndividualMember
{
[BsonElement("member_id")]
public string MemberId { get; set; } = null!;

[BsonElement("member_name")]
public string MemberName { get; set; } = null!;
}

public class GreetingsTimeSchedule
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
