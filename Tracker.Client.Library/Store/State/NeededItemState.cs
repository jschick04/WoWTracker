using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Store.State;

public class NeededItemState : RootState
{
    public NeededItemState(bool isLoading, string? currentErrorMessage, IList<NeededItemResponse>? items) : base(
        isLoading,
        currentErrorMessage) => Items = items;

    public IList<NeededItemResponse>? Items { get; }

    // TODO: Add ItemsToCraft and CanCraft bool if ItemsToCraft.Any
}
