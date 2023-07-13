using Microsoft.AspNetCore.Mvc;
using Tracker.Api.Authorization;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Library.DataAccess;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Controllers.V1;

[Authorize]
public class ProfessionController : BaseApiController
{
    private readonly IProfessionData _data;

    public ProfessionController(IProfessionData data) => _data = data;

    [HttpGet(ApiRoutes.Profession.GetAllUri)]
    public async Task<ActionResult<IEnumerable<string>>> Get()
    {
        var professions = await _data.GetAll();

        return Ok(professions.Select(x => x.Name));
    }
}
