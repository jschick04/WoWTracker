using Fluxor;
using Tracker.Client.Library.Store.State;

namespace Tracker.Client.Library.Store.NeededItem;

public class NeededItemFeature : Feature<NeededItemState>
{
    public override string GetName() => "NeededItem";

    protected override NeededItemState GetInitialState() => new(true, null, null);
}
