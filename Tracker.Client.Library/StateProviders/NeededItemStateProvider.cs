using Fluxor;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Client.Library.Features.NeededItem;

namespace Tracker.Client.Library.StateProviders;

public class NeededItemStateProvider : INeededItemStateProvider
{
    private readonly IDispatcher _dispatcher;

    public NeededItemStateProvider(IDispatcher dispatcher) => _dispatcher = dispatcher;

    public void AddNeededItem(int id, string name, NeededItemRequest request) =>
        _dispatcher.Dispatch(new NeededItemAddItemAction(id, name, request));

    public void GetAllNeededItems(int id) => _dispatcher.Dispatch(new NeededItemGetAllAction(id));

    public void RemoveNeededItem(int id, NeededItemRequest request) =>
        _dispatcher.Dispatch(new NeededItemRemoveItemAction(id, request));
}
