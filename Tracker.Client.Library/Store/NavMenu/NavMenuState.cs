using Fluxor;

namespace Tracker.Client.Library.Store.NavMenu;

[FeatureState]
public class NavMenuState
{
    public NavMenuState(bool drawerOpen) => DrawerOpen = drawerOpen;

    private NavMenuState() { }

    public bool DrawerOpen { get; } = true;
}
