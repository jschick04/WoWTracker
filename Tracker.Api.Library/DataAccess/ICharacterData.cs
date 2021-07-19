using System.Collections.Generic;
using System.Threading.Tasks;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess {

    public interface ICharacterData {

        Task Create(CharacterModel model);

        Task Delete(int id);

        Task<List<CharacterModel>> GetAll(int userId);

        Task<CharacterModel> GetById(int id, int userId);

        Task Update(CharacterModel model);

    }

}