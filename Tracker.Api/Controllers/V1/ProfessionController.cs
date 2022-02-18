using Microsoft.AspNetCore.Mvc;
using Tracker.Api.Authorization;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Library.DataAccess;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Controllers.V1;

[Authorize]
public class ProfessionController : BaseApiController {

    private readonly IProfessionData _data;

    public ProfessionController(IProfessionData data) => _data = data;

    [HttpGet(ApiRoutes.Profession.GetAll)]
    public async Task<ActionResult<IEnumerable<ProfessionModel>>> Get() => await _data.GetAll();

}