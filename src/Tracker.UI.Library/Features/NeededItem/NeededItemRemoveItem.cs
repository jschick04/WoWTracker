using System.Net.Http.Json;
using Blazored.Toast.Services;
using Fluxor;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.UI.Library.Features.State;

namespace Tracker.UI.Library.Features.NeededItem;

public class NeededItemRemoveItemEffects
{
    private readonly HttpClient _httpClient;
    private readonly IToastService _toastService;

    public NeededItemRemoveItemEffects(HttpClient httpClient, IToastService toastService)
    {
        _httpClient = httpClient;
        _toastService = toastService;
    }

    [EffectMethod]
    public async Task RemoveItemAsync(NeededItemRemoveItemAction action, IDispatcher dispatcher)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(
                ApiRoutes.Character.RemoveNeededItem(action.Id),
                action.Request);

            response.EnsureSuccessStatusCode();

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
        state with { CurrentErrorMessage = string.Empty, IsLoading = true };

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
