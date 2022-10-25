using Fluxor;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Client.Library.Store.Character.Create;
using Tracker.Client.Library.Store.Character.GetAll;
using Tracker.Client.Library.Store.Character.SetSelected;
using Tracker.Client.Library.Store.Character.UpdateSelected;

namespace Tracker.Client.Library.StateProviders;

public class CharacterStateProvider : ICharacterStateProvider
{
    private readonly IDispatcher _dispatcher;

    public CharacterStateProvider(IDispatcher dispatcher) => _dispatcher = dispatcher;

    public void CreateCharacter(CreateCharacterRequest request) => _dispatcher.Dispatch(new CreateAction(request));

    public void GetAllCharacters() => _dispatcher.Dispatch(new GetAllAction());

    public void SetSelectedCharacter(int id) => _dispatcher.Dispatch(new SetSelectedAction(id));

    public void UpdateSelectedCharacter(int id, UpdateCharacterRequest request) =>
        _dispatcher.Dispatch(new UpdateSelectedAction(id, request));
}
