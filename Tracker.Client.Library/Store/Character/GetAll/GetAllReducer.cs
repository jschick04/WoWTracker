using Fluxor;
using Tracker.Client.Library.Store.State;

namespace Tracker.Client.Library.Store.Character.GetAll;

public class GetAllReducer
{
    [ReducerMethod(typeof(GetAllAction))]
    public static CharacterState ReducerGetAllAction(CharacterState state) => new(true, null, state.Characters, state.Selected);

    [ReducerMethod]
    public static CharacterState ReducerGetAllFailureAction(CharacterState state, GetAllFailureAction action) =>
        new(false, action.ErrorMessage, state.Characters, state.Selected);

    [ReducerMethod]
    public static CharacterState ReducerGetAllSuccessAction(CharacterState state, GetAllSuccessAction action) =>
        new(false, null, action.Characters, state.Selected);
}
