using System.Collections.Generic;
using System.Threading.Tasks;
using TrackerApi.Library.Database;
using TrackerApi.Library.Models;

namespace TrackerApi.Library.DataAccess {

    public class ItemData : IItemData {

        private readonly ISqlDataAccess _db;

        public ItemData(ISqlDataAccess db) {
            _db = db;
        }

        public Task<List<ItemModel>> GetItemsByProfession(string profession) =>
            _db.LoadData<ItemModel, dynamic>("spItems_GetByProfession", new { name = profession });

        public Task<List<ItemModel>> GetItemsBySlot(string slot) =>
            _db.LoadData<ItemModel, dynamic>("spItems_GetBySlot", new { name = slot });

    }

}