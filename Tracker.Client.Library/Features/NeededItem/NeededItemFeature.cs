using Fluxor;
using Tracker.Client.Library.Features.State;

namespace Tracker.Client.Library.Features.NeededItem;

public class NeededItemFeature : Feature<NeededItemState>
{
    public override string GetName() => nameof(NeededItemFeature);

    protected override NeededItemState GetInitialState() => new() { IsLoading = true };
}

public class NeededItemReducers
{
    [ReducerMethod(typeof(ClearLoadingAction))]
    public static NeededItemState OnClearLoading(NeededItemState state) => state with { IsLoading = false };
}

#region Actions

public record ClearLoadingAction;

#endregion
