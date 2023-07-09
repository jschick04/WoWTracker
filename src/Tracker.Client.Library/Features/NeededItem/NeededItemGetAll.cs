using Fluxor;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Client.Library.Features.State;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Features.NeededItem;

public class NeededItemGetAllEffects
{
    private readonly ICharacterManager _characterManager;

    public NeededItemGetAllEffects(ICharacterManager characterManager) => _characterManager = characterManager;

    [EffectMethod]
    public async Task GetAllAsync(NeededItemGetAllAction action, IDispatcher dispatcher)
    {
        try
        {
            var items = await _characterManager.GetNeededItemsAsync(action.Id);

            if (items.Succeeded is not true || items.Data is null)
            {
                throw new Exception(items.Message);
            }

            dispatcher.Dispatch(new NeededItemGetAllSuccessAction(items.Data));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new NeededItemGetAllFailureAction(ex.Message));
        }

        dispatcher.Dispatch(new ClearLoadingAction());
    }
}

public class NeededItemGetAllReducers
{
    [ReducerMethod(typeof(NeededItemGetAllAction))]
    public static NeededItemState OnGetAll(NeededItemState state) =>
        state with { CurrentErrorMessage = null, IsLoading = true };

    [ReducerMethod]
    public static NeededItemState OnGetAllFailure(NeededItemState state, NeededItemGetAllFailureAction action) =>
        state with { CurrentErrorMessage = action.ErrorMessage };

    [ReducerMethod]
    public static NeededItemState OnGetAllSuccess(NeededItemState state, NeededItemGetAllSuccessAction action) =>
        state with { Items = action.Items };
}

#region Actions

public record NeededItemGetAllAction(int Id);

public record NeededItemGetAllFailureAction(string ErrorMessage) : FailureAction(ErrorMessage);

public record NeededItemGetAllSuccessAction(IEnumerable<NeededItemResponse> Items);

#endregion
