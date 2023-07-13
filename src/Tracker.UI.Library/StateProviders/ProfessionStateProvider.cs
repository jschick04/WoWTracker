using Fluxor;
using Tracker.UI.Library.Features.Profession;

namespace Tracker.UI.Library.StateProviders;

public class ProfessionStateProvider : IProfessionStateProvider
{
    private readonly IDispatcher _dispatcher;

    public ProfessionStateProvider(IDispatcher dispatcher) => _dispatcher = dispatcher;

    public void GetAllCraftableItems() => _dispatcher.Dispatch(new ProfessionGetAllItemsAction());
}
