using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackerApi.Authorization;
using TrackerApi.Contracts.V1;
using TrackerApi.Library.DataAccess;
using TrackerApi.Library.Models;

namespace TrackerApi.Controllers.V1 {

    [Authorize]
    public class CharacterController : BaseApiController {

        private readonly ICharacterData _data;

        public CharacterController(ICharacterData data) => _data = data;

        [HttpGet(ApiRoutes.Character.GetAll)]
        public async Task<ActionResult<IEnumerable<CharacterModel>>> GetAll() => await _data.GetAll();

        [HttpGet(ApiRoutes.Character.GetById)]
        public async Task<ActionResult<IEnumerable<CharacterModel>>> GetById([FromRoute] int id) =>
            await _data.GetById(id);

    }

}