using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackerApi.Library.DataAccess;
using TrackerApi.Library.Models;

namespace TrackerApi.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase {

        private readonly IItemData _data;

        public ItemController(IItemData data) {
            _data = data;
        }

        // GET: api/Item/Profession/{profession}
        [HttpGet("Profession/{profession}")]
        public async Task<List<ItemModel>> GetByProfession(string profession) =>
            await _data.GetItemsByProfession(profession);

        // GET: api/Item/Slot/{name}
        [HttpGet("Slot/{name}")]
        public async Task<List<ItemModel>> GetBySlot(string name) => await _data.GetItemsBySlot(name);

    }

}