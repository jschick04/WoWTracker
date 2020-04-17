using System.Collections.Generic;
using System.Threading.Tasks;
using TrackerApi.Library.Models;

namespace TrackerApi.Library.DataAccess {

    public interface ICharacterData {

        Task<List<CharacterModel>> GetCharacterByName(string name);

        Task<List<CharacterModel>> GetCharacters();

    }

}