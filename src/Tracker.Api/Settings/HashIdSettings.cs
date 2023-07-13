namespace Tracker.Api.Settings;

public record HashIdSettings
{
    public string Secret { get; init; } = null!;
}
