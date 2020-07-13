using System.Collections.Generic;
using System.Threading.Tasks;
using TrackerApi.Library.Database;
using TrackerApi.Library.Models;

namespace TrackerApi.Library.DataAccess {

    public class ProfessionData : IProfessionData {

        private readonly ISqlDataAccess _db;

        public ProfessionData(ISqlDataAccess db) {
            _db = db;
        }

        public Task<List<ProfessionModel>> GetAll() =>
            _db.LoadData<ProfessionModel, dynamic>("spProfessions_GetAll", new { });

    }

}