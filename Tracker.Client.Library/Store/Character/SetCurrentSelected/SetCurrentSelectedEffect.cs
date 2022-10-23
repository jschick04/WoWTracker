using Fluxor;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Store.Character.SetCurrentSelected;

public class SetCurrentSelectedEffect : Effect<SetCurrentSelectedAction>
{
    private readonly ICharacterManager _characterManager;

    public SetCurrentSelectedEffect(ICharacterManager characterManager) => _characterManager = characterManager;

    public override async Task HandleAsync(SetCurrentSelectedAction action, IDispatcher dispatcher)
    {
        try
        {
            var character = await _characterManager.GetByIdAsync(action.Id);

            if (character.Succeeded is not true || character.Data is null)
            {
                throw new Exception(character.Message);
            }

            dispatcher.Dispatch(new SetCurrentSelectedSuccessAction(character.Data));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new SetCurrentSelectedFailureAction(ex.Message));
        }
    }
}
