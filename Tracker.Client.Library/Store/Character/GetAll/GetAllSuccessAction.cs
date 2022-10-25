using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Store.Character.GetAll;

public class GetAllSuccessAction
{
    public GetAllSuccessAction(IList<CharacterResponse> characters) => Characters = characters;

    public IList<CharacterResponse> Characters { get; }
}
