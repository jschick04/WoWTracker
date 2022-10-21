using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Store.Character;

public class FetchDataResultAction
{
    public FetchDataResultAction(IEnumerable<CharacterResponse> characters) => Characters = characters;

    public IEnumerable<CharacterResponse> Characters { get; set; }
}
