namespace Tracker.Api.Settings;

public record SwaggerSettings
{
    public string Description { get; init; } = null!;

    public string JsonRoute { get; init; } = null!;

    public string UiEndpoint { get; init; } = null!;
}
