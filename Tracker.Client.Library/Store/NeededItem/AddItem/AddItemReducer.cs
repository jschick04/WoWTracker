using Fluxor;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Client.Library.Store.State;

namespace Tracker.Client.Library.Store.NeededItem.AddItem;

public class AddItemReducer
{
    [ReducerMethod(typeof(AddItemAction))]
    public static NeededItemState ReducerAddItemAction(NeededItemState state) => new(true, null, state.Items);

    [ReducerMethod]
    public static NeededItemState ReducerAddItemFailureAction(NeededItemState state, AddItemFailureAction action) =>
        new(false, action.ErrorMessage, state.Items);

    [ReducerMethod]
    public static NeededItemState ReducerAddItemSuccessAction(NeededItemState state, AddItemSuccessAction action)
    {
        if (state.Items is null)
        {
            return new NeededItemState(false, null, new List<NeededItemResponse> { action.Item });
        }

        var item = state.Items.FirstOrDefault(item => Equals(item.Name, action.Item.Name));

        if (item is null)
        {
            state.Items.Add(action.Item);
        }
        else
        {
            item.Amount += action.Item.Amount;
        }

        return new NeededItemState(false, null, state.Items);
    }
}
