using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Library.Store.NeededItem.GetAll;

public class GetAllSuccessAction
{
    public GetAllSuccessAction(IList<NeededItemResponse> items) => Items = items;

    public IList<NeededItemResponse> Items { get; }
}
