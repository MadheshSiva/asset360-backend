namespace A360.VisitorManagement.Api.Settings;

public sealed class GatePassNotificationSettings
{
    public const string SectionName = "GatePassNotification";

    public string PortalUrl { get; init; } = string.Empty;

    public string ApiBaseUrl { get; init; } = string.Empty;

    public string LinkSecret { get; init; } = string.Empty;

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(PortalUrl))
        {
            throw new InvalidOperationException("Gate pass portal URL is required.");
        }

        if (string.IsNullOrWhiteSpace(ApiBaseUrl))
        {
            throw new InvalidOperationException("Gate pass API base URL is required.");
        }

        if (string.IsNullOrWhiteSpace(LinkSecret))
        {
            throw new InvalidOperationException("Gate pass approval link secret is required.");
        }
    }
}
