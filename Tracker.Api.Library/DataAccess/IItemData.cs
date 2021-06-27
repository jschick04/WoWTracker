using System.Collections.Generic;
using System.Threading.Tasks;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess {

    public interface IItemData {

        Task<List<ItemModel>> GetItemsByProfession(string profession);

        Task<List<ItemModel>> GetItemsBySlot(string slot);

    }

}