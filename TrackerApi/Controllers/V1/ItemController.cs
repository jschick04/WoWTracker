using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackerApi.Authorization;
using TrackerApi.Contracts.V1;
using TrackerApi.Library.DataAccess;
using TrackerApi.Library.Models;

namespace TrackerApi.Controllers.V1 {

    [Authorize]
    public class ItemController : BaseApiController {

        private readonly IItemData _data;

        public ItemController(IItemData data) => _data = data;

        [HttpGet(ApiRoutes.Item.GetByProfession)]
        public async Task<ActionResult<IEnumerable<ItemModel>>> GetByProfession(string profession) =>
            await _data.GetItemsByProfession(profession);

        [HttpGet(ApiRoutes.Item.GetBySlot)]
        public async Task<ActionResult<IEnumerable<ItemModel>>> GetBySlot(string name) =>
            await _data.GetItemsBySlot(name);

    }

}