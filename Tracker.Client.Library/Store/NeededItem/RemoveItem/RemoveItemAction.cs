using Tracker.Api.Contracts.V1.Requests;

namespace Tracker.Client.Library.Store.NeededItem.RemoveItem;

public class RemoveItemAction
{
    public RemoveItemAction(int id, NeededItemRequest request)
    {
        Id = id;
        Request = request;
    }

    public int Id { get; }

    public NeededItemRequest Request { get; }
}
