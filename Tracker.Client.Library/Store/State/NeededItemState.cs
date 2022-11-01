using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Store.State;

public class NeededItemState : RootState
{
    public NeededItemState(bool isLoading, string? currentErrorMessage, IEnumerable<NeededItemResponse>? items)
        : base(isLoading, currentErrorMessage) => Items = items ?? Enumerable.Empty<NeededItemResponse>();

    public IEnumerable<NeededItemResponse> Items { get; }
}
