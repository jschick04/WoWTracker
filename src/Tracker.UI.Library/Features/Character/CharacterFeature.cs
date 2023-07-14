using Fluxor;
using Tracker.UI.Library.Features.State;

namespace Tracker.UI.Library.Features.Character;

public class CharacterFeature : Feature<CharacterState>
{
    public override string GetName() => nameof(CharacterFeature);

    protected override CharacterState GetInitialState() => new() { IsLoading = true };
}

public class CharacterReducers
{
    [ReducerMethod(typeof(ClearLoadingAction))]
    public static CharacterState OnClearLoading(CharacterState state) => state with { IsLoading = false };

    [ReducerMethod(typeof(ClearRefreshingAction))]
    public static CharacterState OnClearRefreshing(CharacterState state) => state with { IsRefreshing = false };
}

#region Actions

public record ClearLoadingAction;

public record ClearRefreshingAction;

#endregion
