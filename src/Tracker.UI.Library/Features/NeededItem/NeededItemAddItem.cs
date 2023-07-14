using System.Net.Http.Json;
using Blazored.Toast.Services;
using Fluxor;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.UI.Library.Features.State;

namespace Tracker.UI.Library.Features.NeededItem;

public class NeededItemAddItemEffects
{
    private readonly HttpClient _httpClient;
    private readonly IToastService _toastService;

    public NeededItemAddItemEffects(HttpClient httpClient, IToastService toastService)
    {
        _httpClient = httpClient;
        _toastService = toastService;
    }

    [EffectMethod]
    public async Task AddItemAsync(NeededItemAddItemAction action, IDispatcher dispatcher)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(
                ApiRoutes.Character.AddNeededItem(action.Id),
                action.Request);

            response.EnsureSuccessStatusCode();

            var newItem = new NeededItemResponse
            {
                CharacterId = action.Id,
                CharacterName = action.CharacterName,
                Profession = action.Request.Profession,
                Name = action.Request.Name,
                Amount = action.Request.Amount
            };

            _toastService.ShowSuccess($"{newItem.Name} has been added");
            dispatcher.Dispatch(new NeededItemAddItemSuccessAction(newItem));
        }
        catch (Exception ex)
        {
            _toastService.ShowError(ex.Message);
            dispatcher.Dispatch(new NeededItemAddItemFailureAction(ex.Message));
        }

        dispatcher.Dispatch(new ClearLoadingAction());
    }
}

public class NeededItemAddItemReducers
{
    [ReducerMethod(typeof(NeededItemAddItemAction))]
    public static NeededItemState OnAddItem(NeededItemState state) =>
        state with { CurrentErrorMessage = string.Empty, IsLoading = true };

    [ReducerMethod]
    public static NeededItemState OnAddItemFailure(NeededItemState state, NeededItemAddItemFailureAction action) =>
        state with { CurrentErrorMessage = action.ErrorMessage };

    [ReducerMethod]
    public static NeededItemState OnAddItemSuccess(NeededItemState state, NeededItemAddItemSuccessAction action)
    {
        var updatedList = state.Items.ToList();
        var item = updatedList.FirstOrDefault(item => Equals(item.Name, action.Item.Name));

        if (item is null)
        {
            updatedList.Add(action.Item);
        }
        else
        {
            item.Amount += action.Item.Amount;
        }

        return state with { Items = updatedList };
    }
}

#region Actions

public record NeededItemAddItemAction(string Id, string CharacterName, NeededItemRequest Request);

public record NeededItemAddItemFailureAction(string ErrorMessage) : FailureAction(ErrorMessage);

public record NeededItemAddItemSuccessAction(NeededItemResponse Item);

#endregion
