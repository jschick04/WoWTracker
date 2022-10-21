using Fluxor;
using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Store.Character;

[FeatureState]
public class CharacterState
{
    public CharacterState(bool isLoading, IEnumerable<CharacterResponse>? characters)
    {
        IsLoading = isLoading;
        Characters = characters ?? Array.Empty<CharacterResponse>();
    }

    private CharacterState() { }

    public IEnumerable<CharacterResponse> Characters { get; set; } = null!;

    public bool IsLoading { get; set; } = true;
}
