using System.Collections.Generic;
using System.Threading.Tasks;
using Tracker.Api.Library.Database;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess {

    public class ProfessionData : IProfessionData {

        private readonly ISqlDataAccess _db;

        public ProfessionData(ISqlDataAccess db) => _db = db;

        public Task<List<ProfessionModel>> GetAll() =>
            _db.LoadData<ProfessionModel, dynamic>("spProfessions_GetAll", new { });

    }

}