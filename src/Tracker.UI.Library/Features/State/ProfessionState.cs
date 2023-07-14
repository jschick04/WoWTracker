using System.Collections.Immutable;
using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.UI.Library.Features.State;

public record ProfessionState : RootState
{
    public IReadOnlyDictionary<string, ImmutableList<ItemResponse>> Items { get; init; } =
        ImmutableDictionary<string, ImmutableList<ItemResponse>>.Empty;
}
