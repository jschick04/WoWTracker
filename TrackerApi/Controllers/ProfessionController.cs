using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackerApi.Library.DataAccess;
using TrackerApi.Library.Models;

namespace TrackerApi.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionController : ControllerBase {

        private readonly IProfessionData _data;

        public ProfessionController(IProfessionData data) {
            _data = data;
        }

        // GET: api/Profession
        [HttpGet]
        public async Task<List<ProfessionModel>> Get() => await _data.GetAll();

    }

}