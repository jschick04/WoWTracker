using System.Collections.Generic;
using System.Threading.Tasks;
using Tracker.Api.Library.Database;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess {

    public class ItemData : IItemData {

        private readonly ISqlDataAccess _db;

        public ItemData(ISqlDataAccess db) => _db = db;

        public Task<List<ItemModel>> GetItemsByProfession(string profession) =>
            _db.LoadData<ItemModel, dynamic>("spItems_GetByProfession", new { name = profession });

        public Task<List<ItemModel>> GetItemsBySlot(string slot) =>
            _db.LoadData<ItemModel, dynamic>("spItems_GetBySlot", new { name = slot });

    }

}