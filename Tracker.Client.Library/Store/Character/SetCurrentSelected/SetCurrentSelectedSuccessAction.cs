using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Store.Character.SetCurrentSelected;

public class SetCurrentSelectedSuccessAction
{
    public SetCurrentSelectedSuccessAction(CharacterResponse character) => Character = character;

    public CharacterResponse Character { get; set; }
}
