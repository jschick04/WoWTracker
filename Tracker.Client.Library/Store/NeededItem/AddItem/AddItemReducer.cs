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
        if (state.Items.Any() is false)
        {
            return new NeededItemState(false, null, new List<NeededItemResponse> { action.Item });
        }

        var updatedList = state.Items.ToList();
        var item = updatedList.FirstOrDefault(item => Equals(item.Name, action.Item.Name));

        if (item is null)
        {
            updatedList.Add(action.Item);
        }
        else
        {
            item.Amount += action.Item.Amount;
        }

        return new NeededItemState(false, null, updatedList);
    }
}
