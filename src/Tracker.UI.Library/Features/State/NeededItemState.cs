using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Features.State;

public record NeededItemState : RootState
{
    public IEnumerable<NeededItemResponse> Items { get; init; } = Enumerable.Empty<NeededItemResponse>();
}
