using Fluxor;
using Tracker.Client.Library.Store.State;

namespace Tracker.Client.Library.Store.CraftedItem.GetAll;

public class GetAllReducer
{
    [ReducerMethod(typeof(GetAllAction))]
    public static CraftedItemState ReducerGetAllAction(CraftedItemState state) => new(true, null, state.Items);

    [ReducerMethod]
    public static CraftedItemState ReducerGetAllFailureAction(CraftedItemState state, GetAllFailureAction action) =>
        new(false, action.ErrorMessage, state.Items);

    [ReducerMethod]
    public static CraftedItemState ReducerGetAllSuccessAction(CraftedItemState state, GetAllSuccessAction action) =>
        new(false, null, action.Items);
}
