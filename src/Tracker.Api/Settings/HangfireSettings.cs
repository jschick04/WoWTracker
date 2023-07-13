namespace Tracker.Api.Settings;

public record HangfireSettings
{
    public string Username { get; init; } = null!;

    public string Password { get; init; } = null!;
}
