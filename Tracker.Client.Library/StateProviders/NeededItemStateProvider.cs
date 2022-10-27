using Fluxor;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Client.Library.Store.NeededItem.AddItem;
using Tracker.Client.Library.Store.NeededItem.GetAll;

namespace Tracker.Client.Library.StateProviders;

public class NeededItemStateProvider : INeededItemStateProvider
{
    private readonly IDispatcher _dispatcher;

    public NeededItemStateProvider(IDispatcher dispatcher) => _dispatcher = dispatcher;

    public void GetAllNeededItems(int id) => _dispatcher.Dispatch(new GetAllAction(id));

    public void AddNeededItem(int id, string name, NeededItemRequest request) =>
        _dispatcher.Dispatch(new AddItemAction(id, name, request));
}
