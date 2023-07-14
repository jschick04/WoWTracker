using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.UI.Library.Features.State;

public record CraftedItemState : RootState
{
    public IEnumerable<NeededItemResponse> Items { get; init; } = Enumerable.Empty<NeededItemResponse>();
}
