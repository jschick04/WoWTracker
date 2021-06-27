using System.Collections.Generic;
using System.Threading.Tasks;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess {

    public interface ICharacterData {

        Task<List<CharacterModel>> GetAll();

        Task<List<CharacterModel>> GetById(int id);

    }

}