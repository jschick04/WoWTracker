using FluentResults;
using Tracker.Api.Library.Database;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess;

public sealed class ProfessionData : IProfessionData
{
    private readonly ISqlDataAccess _db;

    public ProfessionData(ISqlDataAccess db) => _db = db;

    public async Task<IResult<IEnumerable<ProfessionModel>>> GetAll()
    {
        try
        {
            return Result.Ok(await _db.LoadData<ProfessionModel, dynamic>("spProfessions_GetAll", new { }));
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<ProfessionModel>>(ex.Message);
        }
    }
}
