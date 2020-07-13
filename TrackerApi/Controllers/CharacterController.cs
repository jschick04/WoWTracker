using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackerApi.Library.DataAccess;
using TrackerApi.Library.Models;

namespace TrackerApi.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase {

        private readonly ICharacterData _data;

        public CharacterController(ICharacterData data) {
            _data = data;
        }

        // GET: api/Character
        [HttpGet]
        public async Task<List<CharacterModel>> Get() => await _data.GetCharacters();

        // GET: api/Character/{name}
        [HttpGet("{name}")]
        public async Task<List<CharacterModel>> Get(string name) => await _data.GetCharacterByName(name);

    }

}