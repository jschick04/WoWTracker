using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Store.Character.Create;

public class CreateSuccessAction
{
    public CreateSuccessAction(CharacterResponse character) => Character = character;

    public CharacterResponse Character { get; }
}
