using Fluxor;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.UI.Library.Features.NeededItem;

namespace Tracker.UI.Library.StateProviders;

public class NeededItemStateProvider : INeededItemStateProvider
{
    private readonly IDispatcher _dispatcher;

    public NeededItemStateProvider(IDispatcher dispatcher) => _dispatcher = dispatcher;

    public void AddNeededItem(string id, string name, NeededItemRequest request) =>
        _dispatcher.Dispatch(new NeededItemAddItemAction(id, name, request));

    public void GetAllNeededItems(string id) => _dispatcher.Dispatch(new NeededItemGetAllAction(id));

    public void RemoveNeededItem(string id, NeededItemRequest request) =>
        _dispatcher.Dispatch(new NeededItemRemoveItemAction(id, request));
}
