namespace Tracker.UI.Library.Features;

public abstract record RootState
{
    public string CurrentErrorMessage { get; init; } = string.Empty;

    public bool HasCurrentErrors => !string.IsNullOrWhiteSpace(CurrentErrorMessage);

    public bool IsLoading { get; init; }
}
