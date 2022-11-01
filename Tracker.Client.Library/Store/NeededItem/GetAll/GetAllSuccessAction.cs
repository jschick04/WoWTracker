using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Store.NeededItem.GetAll;

public class GetAllSuccessAction
{
    public GetAllSuccessAction(IEnumerable<NeededItemResponse> items) => Items = items;

    public IEnumerable<NeededItemResponse> Items { get; }
}
