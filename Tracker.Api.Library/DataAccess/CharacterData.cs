using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracker.Api.Library.Database;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess {

    public class CharacterData : ICharacterData {

        private readonly ISqlDataAccess _db;

        public CharacterData(ISqlDataAccess db) => _db = db;

        public async Task Create(CharacterModel request) => await _db.SaveData("spCharacters_Insert", request);

        public async Task Delete(int id) => await _db.SaveData("spCharacters_Delete", new { id });

        public async Task<List<CharacterModel>> GetAll(int userId) =>
            await _db.LoadData<CharacterModel, dynamic>("spCharacters_GetAll", new { userId });

        public async Task<CharacterModel> GetById(int id, int userId) =>
            (await _db.LoadData<CharacterModel, dynamic>("spCharacters_GetById", new { id, userId })).FirstOrDefault();

    }

}