using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Store.State;

public class CharacterState : RootState
{
    public CharacterState(bool isLoading,
        string? currentErrorMessage,
        IEnumerable<CharacterResponse>? characters,
        CharacterResponse? selected,
        bool isRefreshing = false)
        : base(isLoading, currentErrorMessage)
    {
        Characters = characters ?? Enumerable.Empty<CharacterResponse>();
        Selected = selected;
        IsRefreshing = isRefreshing;
    }

    public IEnumerable<CharacterResponse> Characters { get; }

    public bool IsRefreshing { get; }

    public CharacterResponse? Selected { get; }
}
