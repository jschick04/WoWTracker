using System.Collections.Generic;
using System.Threading.Tasks;
using TrackerApi.Library.Database;
using TrackerApi.Library.Models;

namespace TrackerApi.Library.DataAccess {

    public class CharacterData : ICharacterData {

        private readonly ISqlDataAccess _db;

        public CharacterData(ISqlDataAccess db) {
            _db = db;
        }

        public Task<List<CharacterModel>> GetCharacterByName(string name) =>
            _db.LoadData<CharacterModel, dynamic>("spCharacters_GetByName", new { name });

        public Task<List<CharacterModel>> GetCharacters() =>
            _db.LoadData<CharacterModel, dynamic>("spCharacters_GetAll", new { });

    }

}