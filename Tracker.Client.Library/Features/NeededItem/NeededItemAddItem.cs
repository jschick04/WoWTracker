using Blazored.Toast.Services;
using Fluxor;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Client.Library.Features.State;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Features.NeededItem;

public class NeededItemAddItemEffects
{
    private readonly ICharacterManager _characterManager;
    private readonly IToastService _toastService;

    public NeededItemAddItemEffects(ICharacterManager characterManager, IToastService toastService)
    {
        _characterManager = characterManager;
        _toastService = toastService;
    }

    [EffectMethod]
    public async Task AddItemAsync(NeededItemAddItemAction action, IDispatcher dispatcher)
    {
        try
        {
            var result = await _characterManager.AddNeededItemAsync(action.Id, action.Request);

            if (result.Succeeded is not true)
            {
                throw new Exception(result.Message);
            }

            var newItem = new NeededItemResponse
            {
                Id = action.Id,
                CharacterName = action.CharacterName,
                Profession = action.Request.Profession,
                Name = action.Request.Name,
                Amount = action.Request.Amount
            };

            _toastService.ShowSuccess($"{action.Request.Name} has been added");
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
        state with { CurrentErrorMessage = null, IsLoading = true };

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

public record NeededItemAddItemAction(int Id, string CharacterName, NeededItemRequest Request);

public record NeededItemAddItemFailureAction(string ErrorMessage) : FailureAction(ErrorMessage);

public record NeededItemAddItemSuccessAction(NeededItemResponse Item);

#endregion
