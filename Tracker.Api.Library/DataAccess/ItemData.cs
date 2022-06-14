using Tracker.Api.Library.Database;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess;

public class ItemData : IItemData {

    private readonly ISqlDataAccess _db;

    public ItemData(ISqlDataAccess db) => _db = db;

    public Task<List<ItemModel>> GetByProfession(string profession) =>
        _db.LoadData<ItemModel, dynamic>("spItems_GetByProfession", new { name = profession });

    public Task<List<ItemModel>> GetBySlot(string slot) =>
        _db.LoadData<ItemModel, dynamic>("spItems_GetBySlot", new { name = slot });

    public Task<List<NeededItemModel>> GetCraftableByProfession(int userId, int professionId) =>
        _db.LoadData<NeededItemModel, dynamic>("spNeededItems_GetCraftableByProfession", new { userId, professionId });

}