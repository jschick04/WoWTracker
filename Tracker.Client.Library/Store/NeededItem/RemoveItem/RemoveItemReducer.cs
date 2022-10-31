using Fluxor;
using Tracker.Client.Library.Store.State;

namespace Tracker.Client.Library.Store.NeededItem.RemoveItem;

public class RemoveItemReducer
{
    [ReducerMethod(typeof(RemoveItemAction))]
    public static NeededItemState ReducerRemoveItemAction(NeededItemState state) => new(true, null, state.Items);

    [ReducerMethod]
    public static NeededItemState
        ReducerRemoveItemFailureAction(NeededItemState state, RemoveItemFailureAction action) =>
        new(false, action.ErrorMessage, state.Items);

    [ReducerMethod]
    public static NeededItemState ReducerRemoveItemSuccessAction(NeededItemState state, RemoveItemSuccessAction action)
    {
        var item = state.Items?.FirstOrDefault(item => Equals(item.Name, action.Name));

        if (item is null)
        {
            return new NeededItemState(false, null, state.Items);
        }

        var amount = item.Amount - action.Amount;

        if (amount > 0)
        {
            item.Amount = amount;
        }
        else
        {
            state.Items?.Remove(item);
        }

        return new NeededItemState(false, null, state.Items);
    }
}
