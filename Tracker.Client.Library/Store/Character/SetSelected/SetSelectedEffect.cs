using Fluxor;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Store.Character.SetSelected;

public class SetSelectedEffect : Effect<SetSelectedAction>
{
    private readonly ICharacterManager _characterManager;

    public SetSelectedEffect(ICharacterManager characterManager) => _characterManager = characterManager;

    public override async Task HandleAsync(SetSelectedAction action, IDispatcher dispatcher)
    {
        try
        {
            var character = await _characterManager.GetByIdAsync(action.Id);

            if (character.Succeeded is not true || character.Data is null)
            {
                throw new Exception(character.Message);
            }

            dispatcher.Dispatch(new SetSelectedSuccessAction(character.Data));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new SetSelectedFailureAction(ex.Message));
        }
    }
}
