using Blazored.Toast.Services;
using Fluxor;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Store.NeededItem.RemoveItem;

public class RemoveItemEffect : Effect<RemoveItemAction>
{
    private readonly ICharacterManager _characterManager;
    private readonly IToastService _toastService;

    public RemoveItemEffect(ICharacterManager characterManager, IToastService toastService)
    {
        _characterManager = characterManager;
        _toastService = toastService;
    }

    public override async Task HandleAsync(RemoveItemAction action, IDispatcher dispatcher)
    {
        try
        {
            var result = await _characterManager.RemoveNeededItemAsync(action.Id, action.Request);

            if (result.Succeeded is not true)
            {
                throw new Exception(result.Message);
            }

            _toastService.ShowSuccess($"{action.Request.Name} has been removed");
            dispatcher.Dispatch(new RemoveItemSuccessAction(action.Request.Name, action.Request.Amount));
        }
        catch (Exception ex)
        {
            _toastService.ShowError(ex.Message);
            dispatcher.Dispatch(new RemoveItemFailureAction(ex.Message));
        }
    }
}
