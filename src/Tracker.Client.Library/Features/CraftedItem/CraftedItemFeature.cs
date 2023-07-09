using Fluxor;
using Tracker.Client.Library.Features.State;

namespace Tracker.Client.Library.Features.CraftedItem;

public class CraftedItemFeature : Feature<CraftedItemState>
{
    public override string GetName() => nameof(CraftedItemFeature);

    protected override CraftedItemState GetInitialState() => new() { IsLoading = true };
}

public class CraftedItemReducers
{
    [ReducerMethod(typeof(ClearLoadingAction))]
    public static CraftedItemState OnClearLoading(CraftedItemState state) => state with { IsLoading = false };
}

#region Actions

public record ClearLoadingAction;

#endregion
