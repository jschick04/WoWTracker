using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess;

public interface IItemData {

    Task<List<ItemModel>> GetByProfession(string profession);

    Task<List<ItemModel>> GetBySlot(string slot);

    Task<List<NeededItemModel>> GetCraftableByProfession(int userId, int professionId);

}