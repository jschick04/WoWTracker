namespace Tracker.Api.Library.Database;

public interface ISqlDataAccess
{
    string ConnectionStringName { get; set; }

    Task<List<T>> LoadData<T, TU>(string storedProcedure, TU parameters);

    Task SaveData<T>(string storedProcedure, T parameters);

    Task<T> SaveData<T, TU>(string storedProcedure, TU parameters);
}
