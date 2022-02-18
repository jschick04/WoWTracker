using Microsoft.AspNetCore.Mvc;
using Tracker.Api.Authorization;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Library.DataAccess;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Controllers.V1;

[Authorize]
public class ItemController : BaseApiController {

    private readonly IItemData _data;

    public ItemController(IItemData data) => _data = data;

    [HttpGet(ApiRoutes.Item.GetByProfession)]
    public async Task<ActionResult<IEnumerable<ItemModel>>> GetByProfession(string name) =>
        await _data.GetItemsByProfession(name);

    [HttpGet(ApiRoutes.Item.GetBySlot)]
    public async Task<ActionResult<IEnumerable<ItemModel>>> GetBySlot(string name) => await _data.GetItemsBySlot(name);

}