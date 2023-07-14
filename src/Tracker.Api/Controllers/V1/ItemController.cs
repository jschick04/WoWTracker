using Microsoft.AspNetCore.Mvc;
using Tracker.Api.Authorization;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Api.Library.DataAccess;
using Tracker.Api.Library.Helpers;
using Tracker.Api.Managers;

namespace Tracker.Api.Controllers.V1;

public class ItemController : BaseApiController
{
    private readonly IItemData _data;
    private readonly IHashIdManager _hashIdManager;

    public ItemController(IItemData data, IHashIdManager hashIdManager)
    {
        _data = data;
        _hashIdManager = hashIdManager;
    }

    [HttpGet(ApiRoutes.Item.GetAllUri)]
    public async Task<ActionResult<Dictionary<string, IEnumerable<ItemResponse>>>> GetAll()
    {
        Dictionary<string, IEnumerable<ItemResponse>> response = new();

        foreach (var professionId in (Professions[])Enum.GetValues(typeof(Professions)))
        {
            var profession = professionId.GetName();
            var result = await _data.GetByProfession(profession);

            if (result.IsFailed) { return Problem(result.Errors.FirstOrDefault()?.Message); }

            response[profession] = result.Value.Select(x => new ItemResponse { Name = x.Name });
        }

        return Ok(response);
    }

    [HttpGet(ApiRoutes.Item.GetByProfessionUri)]
    public async Task<ActionResult<IEnumerable<ItemResponse>>> GetByProfession(string name)
    {
        var result = await _data.GetByProfession(name);

        return result switch
        {
            { IsSuccess: true } => Ok(result.Value.Select(x => new ItemResponse { Name = x.Name })),
            { IsFailed: true } => Problem(result.Errors.FirstOrDefault()?.Message),
            _ => Problem()
        };
    }

    [HttpGet(ApiRoutes.Item.GetBySlotUri)]
    public async Task<ActionResult<IEnumerable<ItemResponse>>> GetBySlot(string name)
    {
        var result = await _data.GetBySlot(name);

        return result switch
        {
            { IsSuccess: true } => Ok(result.Value.Select(x => new ItemResponse { Name = x.Name })),
            { IsFailed: true } => Problem(result.Errors.FirstOrDefault()?.Message),
            _ => Problem()
        };
    }

    [Authorize]
    [HttpGet(ApiRoutes.Item.GetCraftableByProfessionUri)]
    public async Task<ActionResult<IEnumerable<NeededItemResponse>>> GetCraftableByProfession(string name)
    {
        if (Account is null) { return Unauthorized(); }

        if (!Converter.TryParseWithMemberName(name, out Professions professionId))
        {
            return NotFound();
        }

        var result = await _data.GetCraftableByProfession(Account.Id, (int)professionId);

        return result switch
        {
            { IsSuccess: true } => Ok(result.Value.Select(
                item => new NeededItemResponse
                {
                    CharacterId = _hashIdManager.Encode(item.CharacterId),
                    CharacterName = item.CharacterName,
                    Profession = item.ProfessionId.GetName(),
                    Name = item.Name,
                    Amount = item.Amount
                }
            )),
            { IsFailed: true } => Problem(result.Errors.FirstOrDefault()?.Message),
            _ => Problem()
        };
    }
}
