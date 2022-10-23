namespace Tracker.Client.Library.StateProviders;

public interface ICharacterStateProvider
{
    void GetAllCharacters();

    void SetSelectedCharacter(int id);
}
