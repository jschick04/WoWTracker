using Blazored.Toast.Services;
using Fluxor;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Store.Character.UpdateSelected;

public class UpdateSelectedEffect : Effect<UpdateSelectedAction>
{
    private readonly ICharacterManager _characterManager;
    private readonly IToastService _toastService;

    public UpdateSelectedEffect(ICharacterManager characterManager, IToastService toastService)
    {
        _characterManager = characterManager;
        _toastService = toastService;
    }

    public override async Task HandleAsync(UpdateSelectedAction action, IDispatcher dispatcher)
    {
        try
        {
            var character = await _characterManager.UpdateAsync(action.Id, action.Request);

            if (character.Succeeded is not true)
            {
                throw new Exception(character.Message);
            }

            _toastService.ShowSuccess($"{action.Request.Name} has been updated");
            dispatcher.Dispatch(new UpdateSelectedSuccessAction(action.Id, action.Request));
        }
        catch (Exception ex)
        {
            _toastService.ShowError(ex.Message);
            dispatcher.Dispatch(new UpdateSelectedFailureAction(ex.Message));
        }
    }
}
