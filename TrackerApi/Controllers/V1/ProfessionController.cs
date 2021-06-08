using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackerApi.Authorization;
using TrackerApi.Contracts.V1;
using TrackerApi.Library.DataAccess;
using TrackerApi.Library.Models;

namespace TrackerApi.Controllers.V1 {

    [Authorize]
    public class ProfessionController : BaseApiController {

        private readonly IProfessionData _data;

        public ProfessionController(IProfessionData data) => _data = data;

        [HttpGet(ApiRoutes.Profession.GetAll)]
        public async Task<ActionResult<IEnumerable<ProfessionModel>>> Get() => await _data.GetAll();

    }

}