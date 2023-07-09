using Fluxor;
using Tracker.Client.Library.Features.State;

namespace Tracker.Client.Library.Features.NavMenu;

public class NavMenuFeature : Feature<NavMenuState>
{
    public override string GetName() => nameof(NavMenuFeature);

    protected override NavMenuState GetInitialState() => new(true);
}

public static class NavMenuReducers
{
    [ReducerMethod(typeof(NavMenuToggleDrawerAction))]
    public static NavMenuState OnToggleDrawer(NavMenuState state) => new(!state.DrawerOpen);
}

#region Actions

public record NavMenuToggleDrawerAction;

#endregion
