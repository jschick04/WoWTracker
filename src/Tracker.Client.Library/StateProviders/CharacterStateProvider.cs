using Fluxor;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Client.Library.Features.Character;

namespace Tracker.Client.Library.StateProviders;

public class CharacterStateProvider : ICharacterStateProvider
{
    private readonly IDispatcher _dispatcher;

    public CharacterStateProvider(IDispatcher dispatcher) => _dispatcher = dispatcher;

    public void CreateCharacter(CreateCharacterRequest request) =>
        _dispatcher.Dispatch(new CharacterCreateAction(request));

    public void DeleteSelectedCharacter(int id) => _dispatcher.Dispatch(new CharacterDeleteSelectedAction(id));

    public void GetAllCharacters() => _dispatcher.Dispatch(new CharacterGetAllAction());

    public void SetSelectedCharacter(int id) => _dispatcher.Dispatch(new CharacterSetSelectedAction(id));

    public void UpdateSelectedCharacter(int id, UpdateCharacterRequest request) =>
        _dispatcher.Dispatch(new CharacterUpdateSelectedAction(id, request));
}
