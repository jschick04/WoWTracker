using Fluxor;
using Tracker.Client.Library.Store.AuthFailure;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Store.Character;

public class Effects
{
    private readonly ICharacterManager _characterManager;

    public Effects(ICharacterManager characterManager) => _characterManager = characterManager;

    [EffectMethod(typeof(FetchDataAction))]
    public async Task HandleFetchDataAction(IDispatcher dispatcher)
    {
        var characters = await _characterManager.GetAllAsync();

        if (characters.Succeeded is not true || characters.Data is null)
        {
            dispatcher.Dispatch(new LogOutAction());
            return;
        }

        dispatcher.Dispatch(new FetchDataResultAction(characters.Data));
    }
}
