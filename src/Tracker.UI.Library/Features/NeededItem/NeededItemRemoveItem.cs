using Blazored.Toast.Services;
using Fluxor;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Client.Library.Features.State;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Features.NeededItem;

public class NeededItemRemoveItemEffects
{
    private readonly ICharacterManager _characterManager;
    private readonly IToastService _toastService;

    public NeededItemRemoveItemEffects(ICharacterManager characterManager, IToastService toastService)
    {
        _characterManager = characterManager;
        _toastService = toastService;
    }

    [EffectMethod]
    public async Task RemoveItemAsync(NeededItemRemoveItemAction action, IDispatcher dispatcher)
    {
        try
        {
            var result = await _characterManager.RemoveNeededItemAsync(action.Id, action.Request);

            if (result.Succeeded is not true)
            {
                throw new Exception(result.Message);
            }

            _toastService.ShowSuccess($"{action.Request.Name} has been removed");
            dispatcher.Dispatch(new NeededItemRemoveItemSuccessAction(action.Request.Name, action.Request.Amount));
        }
        catch (Exception ex)
        {
            _toastService.ShowError(ex.Message);
            dispatcher.Dispatch(new NeededItemRemoveItemFailureAction(ex.Message));
        }

        dispatcher.Dispatch(new ClearLoadingAction());
    }
}

public class NeededItemRemoveItemReducers
{
    [ReducerMethod(typeof(NeededItemRemoveItemAction))]
    public static NeededItemState OnRemoveItem(NeededItemState state) =>
        state with { CurrentErrorMessage = null, IsLoading = true };

    [ReducerMethod]
    public static NeededItemState
        OnRemoveItemFailure(NeededItemState state, NeededItemRemoveItemFailureAction action) =>
        state with { CurrentErrorMessage = action.ErrorMessage };

    [ReducerMethod]
    public static NeededItemState OnRemoveItemSuccess(NeededItemState state, NeededItemRemoveItemSuccessAction action)
    {
        var updatedList = state.Items.ToList();
        var item = updatedList.FirstOrDefault(item => Equals(item.Name, action.Name));

        if (item is null) { return state; }

        var amount = item.Amount - action.Amount;

        if (amount > 0)
        {
            item.Amount = amount;
        }
        else
        {
            updatedList.Remove(item);
        }

        return state with { Items = updatedList };
    }
}

#region Actions

public record NeededItemRemoveItemAction(int Id, NeededItemRequest Request);

public record NeededItemRemoveItemFailureAction(string ErrorMessage) : FailureAction(ErrorMessage);

public record NeededItemRemoveItemSuccessAction(string Name, int Amount);

#endregion
