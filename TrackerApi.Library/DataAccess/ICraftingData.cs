using System.Collections.Generic;
using System.Threading.Tasks;
using TrackerApi.Library.Models;

namespace TrackerApi.Library.DataAccess {
    public interface ICraftingData {
        Task<List<CraftingModel>> GetItemsToCraft(string profession);
    }
}