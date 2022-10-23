using Fluxor;
using Tracker.Client.Library.Store.State;

namespace Tracker.Client.Library.Store.Character.SetCurrentSelected;

public class SetCurrentSelectedReducer
{
    [ReducerMethod(typeof(SetCurrentSelectedAction))]
    public static CharacterState ReducerSetCurrentSelectedAction(CharacterState state) =>
        new(false, null, state.Characters, null);

    [ReducerMethod]
    public static CharacterState ReducerSetCurrentSelectedFailureAction(CharacterState state,
        SetCurrentSelectedFailureAction action) => new(false, action.ErrorMessage, state.Characters, state.Selected);

    [ReducerMethod]
    public static CharacterState ReducerSetCurrentSelectedSuccessAction(CharacterState state,
        SetCurrentSelectedSuccessAction action) => new(false, null, state.Characters, action.Character);
}
