using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess;

public interface ICharacterData
{
    Task AddNeededItem(NeededItemModel model);

    Task<int> Create(CharacterModel model);

    Task Delete(int id);

    Task<List<CharacterModel>> GetAll(int userId);

    Task<CharacterModel?> GetById(int id, int userId);

    Task<List<NeededItemModel>> GetNeededItems(int id);

    Task RemoveNeededItem(NeededItemModel model);

    Task Update(CharacterModel model);
}
