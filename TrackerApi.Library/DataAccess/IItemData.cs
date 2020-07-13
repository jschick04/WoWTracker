using System.Collections.Generic;
using System.Threading.Tasks;
using TrackerApi.Library.Models;

namespace TrackerApi.Library.DataAccess {

    public interface IItemData {

        Task<List<ItemModel>> GetItemsByProfession(string profession);

        Task<List<ItemModel>> GetItemsBySlot(string slot);

    }

}