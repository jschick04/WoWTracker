using System.Collections.Generic;
using System.Threading.Tasks;
using TrackerApi.Library.Database;
using TrackerApi.Library.Models;

namespace TrackerApi.Library.DataAccess {

    public class CraftingData : ICraftingData {

        private readonly ISqlDataAccess _db;

        public CraftingData(ISqlDataAccess db) {
            _db = db;
        }

        public Task<List<CraftingModel>> GetItemsToCraft(string profession) =>
            _db.LoadData<CraftingModel, dynamic>("spCrafting_GetByProfession", new { profession });

    }

}