using Fluxor;
using Tracker.Client.Library.Store.State;

namespace Tracker.Client.Library.Store.NavMenu;

public static class ToggleDrawerOpenReducer
{
    [ReducerMethod(typeof(ToggleDrawerOpenAction))]
    public static NavMenuState ReduceToggleDrawerOpenAction(NavMenuState state) => new(!state.DrawerOpen);
}
