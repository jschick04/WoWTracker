using Fluxor;

namespace Tracker.Client.Library.Store.NavMenu;

public static class Reducers
{
    [ReducerMethod(typeof(ToggleDrawerOpenAction))]
    public static NavMenuState ReduceToggleDrawerOpenAction(NavMenuState state) => new(!state.DrawerOpen);
}
