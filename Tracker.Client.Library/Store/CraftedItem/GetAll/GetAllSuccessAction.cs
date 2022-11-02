using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Store.CraftedItem.GetAll;

public class GetAllSuccessAction
{
    public GetAllSuccessAction(IEnumerable<NeededItemResponse> items) => Items = items;

    public IEnumerable<NeededItemResponse> Items { get; }
}
