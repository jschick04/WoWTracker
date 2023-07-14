using Microsoft.AspNetCore.Mvc;
using Tracker.Api.Authorization;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Library.DataAccess;

namespace Tracker.Api.Controllers.V1;

[Authorize]
public class ProfessionController : BaseApiController
{
    private readonly IProfessionData _data;

    public ProfessionController(IProfessionData data) => _data = data;

    [HttpGet(ApiRoutes.Profession.GetAllUri)]
    public async Task<ActionResult<IEnumerable<string>>> Get()
    {
        var result = await _data.GetAll();

        return result switch
        {
            { IsSuccess: true } => Ok(result.Value.Select(x => x.Name)),
            { IsFailed: true } => Problem(result.Errors.FirstOrDefault()?.Message),
            _ => Problem()
        };
    }
}
