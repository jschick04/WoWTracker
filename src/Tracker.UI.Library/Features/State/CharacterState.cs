using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.UI.Library.Features.State;

public record CharacterState : RootState
{
    public IEnumerable<CharacterResponse> Characters { get; init; } = Enumerable.Empty<CharacterResponse>();

    public bool IsRefreshing { get; init; }

    public CharacterResponse? Selected { get; init; }
}
