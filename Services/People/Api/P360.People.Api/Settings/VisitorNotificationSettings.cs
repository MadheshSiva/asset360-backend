namespace P360.People.Api.Settings;

public sealed class VisitorNotificationSettings
{
    public const string SectionName = "VisitorNotification";

    public string PortalUrl { get; init; } = string.Empty;

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(PortalUrl))
        {
            throw new InvalidOperationException("Visitor portal URL is required.");
        }
    }
}
