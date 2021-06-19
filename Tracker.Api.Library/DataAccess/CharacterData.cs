using System.Collections.Generic;
using System.Threading.Tasks;
using Tracker.Api.Library.Database;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess {

    public class CharacterData : ICharacterData {

        private readonly ISqlDataAccess _db;

        public CharacterData(ISqlDataAccess db) => _db = db;

        public Task<List<CharacterModel>> GetAll() =>
            _db.LoadData<CharacterModel, dynamic>("spCharacters_GetAll", new { });

        // TODO: Fix the stored procedure to take ID not Name
        public Task<List<CharacterModel>> GetById(int id) =>
            _db.LoadData<CharacterModel, dynamic>("spCharacters_GetByName", new { id });

    }

}