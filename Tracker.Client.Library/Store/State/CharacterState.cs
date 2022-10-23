using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Store.State;

public class CharacterState : RootState
{
    public CharacterState(bool isLoading,
        string? currentErrorMessage,
        IEnumerable<CharacterResponse>? characters,
        CharacterResponse? selected) : base(isLoading, currentErrorMessage)
    {
        Characters = characters;
        Selected = selected;
    }

    public IEnumerable<CharacterResponse>? Characters { get; }

    public CharacterResponse? Selected { get; }
}
