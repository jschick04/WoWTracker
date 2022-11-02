using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Store.NeededItem.AddItem;

public class AddItemSuccessAction
{
    public AddItemSuccessAction(NeededItemResponse item) => Item = item;

    public NeededItemResponse Item { get; }
}
