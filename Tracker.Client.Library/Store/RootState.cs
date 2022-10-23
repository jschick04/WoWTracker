namespace Tracker.Client.Library.Store;

public abstract class RootState
{
    protected RootState(bool isLoading, string? currentErrorMessage)
    {
        IsLoading = isLoading;
        CurrentErrorMessage = currentErrorMessage;
    }

    public string? CurrentErrorMessage { get; }

    public bool HasCurrentErrors => !string.IsNullOrWhiteSpace(CurrentErrorMessage);

    public bool IsLoading { get; }
}
