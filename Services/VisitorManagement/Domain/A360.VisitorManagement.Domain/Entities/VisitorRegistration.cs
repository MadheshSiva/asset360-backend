using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.VisitorManagement.Domain.Entities;

public class VisitorRegistration : BaseEntity
{
    [BsonElement("visitor_type")]
    public string? VisitorType { get; set; }

    [BsonElement("first_name")]
    public string? FirstName { get; set; }

    [BsonElement("last_name")]
    public string? LastName { get; set; }

    [BsonElement("email")]
    public string? Email { get; set; }

    [BsonElement("mobile_no")]
    public string? MobileNo { get; set; }

    [BsonElement("id_type_id")]
    public string? IdTypeId { get; set; }

    [BsonElement("id_type")]
    public string? IdType { get; set; }

    [BsonElement("id_no")]
    public string? IdNo { get; set; }

    [BsonElement("dob")]
    public string? Dob { get; set; }

    [BsonElement("category_id")]
    public string? CategoryId { get; set; }

    [BsonElement("category")]
    public string? Category { get; set; }

    [BsonElement("gender")]
    public string? Gender { get; set; }

    [BsonElement("phone_no")]
    public string? PhoneNo { get; set; }

    [BsonElement("nationality_id")]
    public string? NationalityId { get; set; }

    [BsonElement("nationality")]
    public string? Nationality { get; set; }

    [BsonElement("company_name")]
    public string? CompanyName { get; set; }

    [BsonElement("contact_name")]
    public string? ContactName { get; set; }

    [BsonElement("contact_no")]
    public string? ContactNo { get; set; }

    [BsonElement("company_email")]
    public string? CompanyEmail { get; set; }

    [BsonElement("address")]
    public string? Address { get; set; }

    [BsonElement("telephone")]
    public string? Telephone { get; set; }

    [BsonElement("trade_licenseno")]
    public string? TradeLicenseNo { get; set; }

    [BsonElement("trade_license_expdate")]
    public string? TradeLicenseExpDate { get; set; }

    [BsonElement("Documents")]
    public List<VisitorRegistrationDocument> Documents { get; set; } = [];

    [BsonElement("created_by")]
    public string? CreatedBy { get; set; }

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("modified_by")]
    public string? ModifiedBy { get; set; }

    [BsonElement("modified_at")]
    public DateTime ModifiedAt { get; set; }

    [BsonElement("password")]
    public string? Password { get; set; }

    [BsonElement("password_reset_token")]
    public string? PasswordResetToken { get; set; }

    [BsonElement("password_reset_token_expiration")]
    public DateTime? PasswordResetTokenExpiration { get; set; }

    [BsonElement("forget_password_otp_code")]
    public string? ForgetPasswordOtpCode { get; set; }

    [BsonElement("forget_password_otp_code_expiration")]
    public DateTime? ForgetPasswordOtpCodeExpiration { get; set; }

    [BsonElement("status")]
    public string? Status { get; set; }
}
