using Fluxor;
using Tracker.Client.Library.Store.Character.GetAll;
using Tracker.Client.Library.Store.Character.SetSelected;

namespace Tracker.Client.Library.StateProviders;

public class CharacterStateProvider : ICharacterStateProvider
{
    private readonly IDispatcher _dispatcher;

    public CharacterStateProvider(IDispatcher dispatcher) => _dispatcher = dispatcher;

    public void GetAllCharacters() => _dispatcher.Dispatch(new GetAllAction());

    public void SetSelectedCharacter(int id) => _dispatcher.Dispatch(new SetSelectedAction(id));
}
