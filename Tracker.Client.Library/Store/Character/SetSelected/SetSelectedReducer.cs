using Fluxor;
using Tracker.Client.Library.Store.State;

namespace Tracker.Client.Library.Store.Character.SetSelected;

public class SetSelectedReducer
{
    [ReducerMethod(typeof(SetSelectedAction))]
    public static CharacterState ReducerSetSelectedAction(CharacterState state) =>
        new(false, null, state.Characters, null);

    [ReducerMethod]
    public static CharacterState ReducerSetSelectedFailureAction(CharacterState state,
        SetSelectedFailureAction action) => new(false, action.ErrorMessage, state.Characters, state.Selected);

    [ReducerMethod]
    public static CharacterState ReducerSetSelectedSuccessAction(CharacterState state,
        SetSelectedSuccessAction action) => new(false, null, state.Characters, action.Character);
}
