using Fluxor;
using Tracker.UI.Library.Features.State;

namespace Tracker.UI.Library.Features.Profession;

public class ProfessionFeature : Feature<ProfessionState>
{
    public override string GetName() => nameof(ProfessionFeature);

    protected override ProfessionState GetInitialState() => new() { IsLoading = true };
}

public class ProfessionReducers
{
    [ReducerMethod(typeof(ClearLoadingAction))]
    public static ProfessionState OnClearLoading(ProfessionState state) => state with { IsLoading = false };
}

#region Actions

public record ClearLoadingAction;

#endregion
