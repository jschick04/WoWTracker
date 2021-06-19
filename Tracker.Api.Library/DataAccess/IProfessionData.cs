using System.Collections.Generic;
using System.Threading.Tasks;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess {

    public interface IProfessionData {

        Task<List<ProfessionModel>> GetAll();

    }

}