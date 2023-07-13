namespace Tracker.Api.Settings;

public record JwtSettings
{
    public string Secret { get; init; } = null!;

    public int TokenLifetimeMinutes { get; init; }

    public int RefreshTokenLifetimeDays { get; init; }

    public int RefreshTokenHistoryDays { get; init; }
}
