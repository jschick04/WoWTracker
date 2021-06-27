using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tracker.Api.Library.Database {

    public interface ISqlDataAccess {

        string ConnectionStringName { get; set; }

        Task<List<T>> LoadData<T, TU>(string storedProcedure, TU parameters);

        Task SaveData<T>(string storedProcedure, T parameters);

    }

}