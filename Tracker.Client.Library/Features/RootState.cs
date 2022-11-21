namespace Tracker.Client.Library.Features;

public abstract record RootState
{
    public string? CurrentErrorMessage { get; init; }

    public bool HasCurrentErrors => !string.IsNullOrWhiteSpace(CurrentErrorMessage);

    public bool IsLoading { get; init; }
}
