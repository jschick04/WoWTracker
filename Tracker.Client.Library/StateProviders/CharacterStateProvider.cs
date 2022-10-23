using Fluxor;
using Tracker.Client.Library.Store.Character.GetAll;
using Tracker.Client.Library.Store.Character.SetCurrentSelected;

namespace Tracker.Client.Library.StateProviders;

public class CharacterStateProvider : ICharacterStateProvider
{
    private readonly IDispatcher _dispatcher;

    public CharacterStateProvider(IDispatcher dispatcher) => _dispatcher = dispatcher;

    public void GetAllCharacters() => _dispatcher.Dispatch(new GetAllAction());

    public void SetCurrentSelectedCharacter(int id) => _dispatcher.Dispatch(new SetCurrentSelectedAction(id));
}
