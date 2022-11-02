using Fluxor;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Store.NeededItem.GetAll;

public class GetAllEffect : Effect<GetAllAction>
{
    private readonly ICharacterManager _characterManager;

    public GetAllEffect(ICharacterManager characterManager) => _characterManager = characterManager;

    public override async Task HandleAsync(GetAllAction action, IDispatcher dispatcher)
    {
        try
        {
            var items = await _characterManager.GetNeededItemsAsync(action.Id);

            if (items.Succeeded is not true || items.Data is null)
            {
                throw new Exception(items.Message);
            }

            dispatcher.Dispatch(new GetAllSuccessAction(items.Data));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new GetAllFailureAction(ex.Message));
        }
    }
}
