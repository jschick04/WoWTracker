using Fluxor;
using Tracker.Client.Library.Store.State;

namespace Tracker.Client.Library.Store.CraftedItem;

public class CraftedItemFeature : Feature<CraftedItemState>
{
    public override string GetName() => "CraftedItem";

    protected override CraftedItemState GetInitialState() => new(true, null, null);
}
