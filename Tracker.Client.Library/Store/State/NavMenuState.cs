namespace Tracker.Client.Library.Store.State;

public class NavMenuState
{
    public NavMenuState(bool drawerOpen) => DrawerOpen = drawerOpen;

    public bool DrawerOpen { get; }
}
