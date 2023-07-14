namespace Tracker.Api.Settings;

public record EmailSettings
{
    public string ApiKey { get; init; } = null!;

    public string From { get; init; } = null!;
}
