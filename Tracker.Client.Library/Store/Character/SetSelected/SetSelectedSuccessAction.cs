using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Store.Character.SetSelected;

public class SetSelectedSuccessAction
{
    public SetSelectedSuccessAction(CharacterResponse character) => Character = character;

    public CharacterResponse Character { get; set; }
}
