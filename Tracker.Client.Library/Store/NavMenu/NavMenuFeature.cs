using Fluxor;
using Tracker.Client.Library.Store.State;

namespace Tracker.Client.Library.Store.NavMenu;

public class NavMenuFeature : Feature<NavMenuState>
{
    public override string GetName() => "NavMenu";

    protected override NavMenuState GetInitialState() => new(true);
}
