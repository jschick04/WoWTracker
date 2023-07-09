namespace Tracker.Api.Settings;

public class JwtSettings
{
    public string Secret { get; set; } = null!;

    public int TokenLifetimeMinutes { get; set; }

    public int RefreshTokenLifetimeDays { get; set; }

    public int RefreshTokenHistoryDays { get; set; }
}
