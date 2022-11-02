using Fluxor;
using Tracker.Client.Library.Store.State;

namespace Tracker.Client.Library.Store.NeededItem.GetAll;

public class GetAllReducer
{
    [ReducerMethod(typeof(GetAllAction))]
    public static NeededItemState ReducerGetAllAction(NeededItemState state) => new(true, null, state.Items);

    [ReducerMethod]
    public static NeededItemState ReducerGetAllFailureAction(NeededItemState state, GetAllFailureAction action) =>
        new(false, action.ErrorMessage, state.Items);

    [ReducerMethod]
    public static NeededItemState ReducerGetAllSuccessAction(NeededItemState state, GetAllSuccessAction action) =>
        new(false, null, action.Items);
}
