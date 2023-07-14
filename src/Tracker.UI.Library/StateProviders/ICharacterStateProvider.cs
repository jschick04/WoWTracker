using Tracker.Api.Contracts.V1.Requests;

namespace Tracker.UI.Library.StateProviders;

public interface ICharacterStateProvider
{
    void CreateCharacter(CreateCharacterRequest request);

    void DeleteSelectedCharacter(string id);

    void GetAllCharacters();

    void SetSelectedCharacter(string id);

    void UpdateSelectedCharacter(string id, UpdateCharacterRequest request);
}
