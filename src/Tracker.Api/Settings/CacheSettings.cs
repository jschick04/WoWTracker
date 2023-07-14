namespace Tracker.Api.Settings;

public record CacheSettings
{
    public bool Enabled { get; init; }

    public string ConnectionString { get; init; } = null!;
}
