using Fluxor;
using Tracker.Client.Library.Store.State;

namespace Tracker.Client.Library.Store.Character.UpdateSelected;

public class UpdateSelectedReducer
{
    [ReducerMethod(typeof(UpdateSelectedAction))]
    public static CharacterState ReducerUpdateSelectedAction(CharacterState state) =>
        new(false, null, state.Characters, state.Selected, true);

    [ReducerMethod]
    public static CharacterState ReducerUpdateSelectedFailureAction(CharacterState state,
        UpdateSelectedFailureAction action) => new(false, action.ErrorMessage, state.Characters, state.Selected);

    [ReducerMethod]
    public static CharacterState ReducerUpdateSelectedSuccessAction(CharacterState state,
        UpdateSelectedSuccessAction action)
    {
        var updatedCharacter = state.Characters!.First(c => c.Id == action.Id);

        updatedCharacter.Name = action.Request.Name;
        updatedCharacter.Class = action.Request.Class;
        updatedCharacter.FirstProfession = action.Request.FirstProfession;
        updatedCharacter.SecondProfession = action.Request.SecondProfession;
        updatedCharacter.HasCooking = action.Request.HasCooking;

        return new CharacterState(false, null, state.Characters, updatedCharacter);
    }
}
