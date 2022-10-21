using Tracker.Api.Library.Database;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess;

public class CharacterData : ICharacterData
{
    private readonly ISqlDataAccess _db;

    public CharacterData(ISqlDataAccess db) => _db = db;

    public async Task Create(CharacterModel model) => await _db.SaveData("spCharacters_Insert", model);

    public async Task AddNeededItem(NeededItemModel model) => await _db.SaveData("spNeededItems_Add", model);

    public async Task Delete(int id) => await _db.SaveData("spCharacters_Delete", new { id });

    public async Task RemoveNeededItem(NeededItemModel model) => await _db.SaveData("spNeededItems_Remove", model);

    public async Task<List<CharacterModel>> GetAll(int userId) =>
        await _db.LoadData<CharacterModel, dynamic>("spCharacters_GetAll", new { userId });

    public async Task<CharacterModel?> GetById(int id, int userId) =>
        (await _db.LoadData<CharacterModel, dynamic>("spCharacters_GetById", new { id, userId })).FirstOrDefault();

    public async Task<List<NeededItemModel>> GetNeededItems(int id) =>
        await _db.LoadData<NeededItemModel, dynamic>("spNeededItems_GetByCharacterId", new { id });

    public async Task Update(CharacterModel model) => await _db.SaveData("spCharacters_Update", model);
}
