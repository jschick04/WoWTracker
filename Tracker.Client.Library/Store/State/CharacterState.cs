using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Store.State;

public class CharacterState : RootState
{
    public CharacterState(bool isLoading,
        string? currentErrorMessage,
        IList<CharacterResponse>? characters,
        CharacterResponse? selected,
        bool isRefreshing = false) : base(isLoading, currentErrorMessage)
    {
        Characters = characters;
        Selected = selected;
        IsRefreshing = isRefreshing;
    }

    public IList<CharacterResponse>? Characters { get; }

    public bool IsRefreshing { get; }

    public CharacterResponse? Selected { get; }
}
