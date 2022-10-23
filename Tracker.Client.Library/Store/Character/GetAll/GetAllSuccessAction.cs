using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Store.Character.GetAll;

public class GetAllSuccessAction
{
    public GetAllSuccessAction(IEnumerable<CharacterResponse> characters) => Characters = characters;

    public IEnumerable<CharacterResponse> Characters { get; }
}
