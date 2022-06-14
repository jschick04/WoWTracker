using Microsoft.AspNetCore.Mvc;
using Tracker.Api.Authorization;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Api.Library.DataAccess;
using Tracker.Api.Library.Helpers;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Controllers.V1;

public class ItemController : BaseApiController {

    private readonly IItemData _data;

    public ItemController(IItemData data) => _data = data;

    [HttpGet(ApiRoutes.Item.GetAll)]
    public async Task<ActionResult<Dictionary<string, List<ItemModel>>>> GetAll() {
        Dictionary<string, List<ItemModel>> response = new();

        try {
            foreach (var professionId in (Professions[])Enum.GetValues(typeof(Professions))) {
                var profession = professionId.GetName();
                var items = await _data.GetByProfession(profession);
                response[profession] = items;
            }
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }

        return Ok(response);
    }

    [HttpGet(ApiRoutes.Item.GetByProfession)]
    public async Task<ActionResult<IEnumerable<ItemModel>>> GetByProfession(string name) =>
        await _data.GetByProfession(name);

    [HttpGet(ApiRoutes.Item.GetBySlot)]
    public async Task<ActionResult<IEnumerable<ItemModel>>> GetBySlot(string name) => await _data.GetBySlot(name);

    [Authorize]
    [HttpGet(ApiRoutes.Item.GetCraftableByProfession)]
    public async Task<ActionResult<IEnumerable<NeededItemModel>>> GetCraftableByProfession(string name) {
        if (Account is null) { return Unauthorized(); }

        if (!Converter.TryParseWithMemberName(name, out Professions professionId)) {
            return NotFound();
        }

        var model = await _data.GetCraftableByProfession(Account.Id, (int)professionId);

        var response = model.Select(
            item => new NeededItemResponse {
                Id = item.Id,
                CharacterName = item.CharacterName,
                Profession = item.ProfessionId.GetName(),
                Name = item.Name,
                Amount = item.Amount
            }
        );

        return Ok(response);
    }

}