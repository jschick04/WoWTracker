using Tracker.Api.Contracts.V1.Requests;

namespace Tracker.UI.Library.StateProviders;

public interface ICharacterStateProvider
{
    void CreateCharacter(CreateCharacterRequest request);

    void DeleteSelectedCharacter(int id);

    void GetAllCharacters();

    void SetSelectedCharacter(int id);

    void UpdateSelectedCharacter(int id, UpdateCharacterRequest request);
}
