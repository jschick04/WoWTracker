namespace Tracker.Api.Settings;

public class CacheSettings
{
    public bool Enabled { get; set; }

    public string ConnectionString { get; set; } = null!;
}
