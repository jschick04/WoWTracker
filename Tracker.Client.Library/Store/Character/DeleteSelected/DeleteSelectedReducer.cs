using Fluxor;
using Tracker.Client.Library.Store.State;

namespace Tracker.Client.Library.Store.Character.DeleteSelected;

public class DeleteSelectedReducer
{
    [ReducerMethod(typeof(DeleteSelectedAction))]
    public static CharacterState ReducerDeleteSelectedAction(CharacterState state) =>
        new(false, null, state.Characters, state.Selected, true);

    [ReducerMethod]
    public static CharacterState ReducerDeleteSelectedFailureAction(CharacterState state,
        DeleteSelectedFailureAction action) => new(false, action.ErrorMessage, state.Characters, state.Selected);

    [ReducerMethod]
    public static CharacterState ReducerDeleteSelectedSuccessAction(CharacterState state,
        DeleteSelectedSuccessAction action)
    {
        var updatedList = state.Characters?.Where(c => c.Id != action.Id).ToList();

        return new CharacterState(false, null, updatedList, state.Selected);
    }
}
