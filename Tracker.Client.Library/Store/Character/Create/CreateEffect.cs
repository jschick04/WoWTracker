using Blazored.Toast.Services;
using Fluxor;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Store.Character.Create;

public class CreateEffect : Effect<CreateAction>
{
    private readonly ICharacterManager _characterManager;
    private readonly IToastService _toastService;

    public CreateEffect(ICharacterManager characterManager, IToastService toastService)
    {
        _characterManager = characterManager;
        _toastService = toastService;
    }

    public override async Task HandleAsync(CreateAction action, IDispatcher dispatcher)
    {
        try
        {
            var character = await _characterManager.CreateAsync(action.Request);

            if (character.Succeeded is not true || character.Data is null)
            {
                throw new Exception(character.Message);
            }

            _toastService.ShowSuccess($"{character.Data.Name} has been created");
            dispatcher.Dispatch(new CreateSuccessAction(character.Data));
        }
        catch (Exception ex)
        {
            _toastService.ShowError(ex.Message);
            dispatcher.Dispatch(new CreateFailureAction(ex.Message));
        }
    }
}
