using Fluxor;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Store.Character.GetAll;

public class GetAllEffect : Effect<GetAllAction>
{
    private readonly ICharacterManager _characterManager;

    public GetAllEffect(ICharacterManager characterManager) => _characterManager = characterManager;

    public override async Task HandleAsync(GetAllAction action, IDispatcher dispatcher)
    {
        try
        {
            var characters = await _characterManager.GetAllAsync();

            if (characters.Succeeded is not true || characters.Data is null)
            {
                throw new Exception(characters.Message);
            }

            dispatcher.Dispatch(new GetAllSuccessAction(characters.Data));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new GetAllFailureAction(ex.Message));
        }
    }
}
