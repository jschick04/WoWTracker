using Blazored.Toast.Services;
using Fluxor;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Store.Character.DeleteSelected;

public class DeleteSelectedEffect : Effect<DeleteSelectedAction>
{
    private readonly ICharacterManager _characterManager;
    private readonly IToastService _toastService;

    public DeleteSelectedEffect(ICharacterManager characterManager, IToastService toastService)
    {
        _characterManager = characterManager;
        _toastService = toastService;
    }

    public override async Task HandleAsync(DeleteSelectedAction action, IDispatcher dispatcher)
    {
        try
        {
            var result = await _characterManager.DeleteAsync(action.Id);

            if (result.Succeeded is not true)
            {
                throw new Exception(result.Message);
            }

            _toastService.ShowSuccess("Delete Successful");
            dispatcher.Dispatch(new DeleteSelectedSuccessAction(action.Id));
        }
        catch (Exception ex)
        {
            _toastService.ShowError(ex.Message);
            dispatcher.Dispatch(new DeleteSelectedFailureAction(ex.Message));
        }
    }
}
