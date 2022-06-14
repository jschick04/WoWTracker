using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess;

public interface ICharacterData {

    Task Create(CharacterModel model);

    Task AddNeededItem(NeededItemModel model);

    Task Delete(int id);

    Task RemoveNeededItem(NeededItemModel model);

    Task<List<CharacterModel>> GetAll(int userId);

    Task<CharacterModel?> GetById(int id, int userId);

    Task<List<NeededItemModel>> GetNeededItems(int id);

    Task Update(CharacterModel model);

}