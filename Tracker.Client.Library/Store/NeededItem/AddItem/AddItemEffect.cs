using Blazored.Toast.Services;
using Fluxor;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Store.NeededItem.AddItem;

public class AddItemEffect : Effect<AddItemAction>
{
    private readonly ICharacterManager _characterManager;
    private readonly IToastService _toastService;

    public AddItemEffect(ICharacterManager characterManager, IToastService toastService)
    {
        _characterManager = characterManager;
        _toastService = toastService;
    }

    public override async Task HandleAsync(AddItemAction action, IDispatcher dispatcher)
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
            dispatcher.Dispatch(new AddItemSuccessAction(newItem));
        }
        catch (Exception ex)
        {
            _toastService.ShowError(ex.Message);
            dispatcher.Dispatch(new AddItemFailureAction(ex.Message));
        }
    }
}
