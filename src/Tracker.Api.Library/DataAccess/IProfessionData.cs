using FluentResults;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess;

public interface IProfessionData
{
    Task<IResult<IEnumerable<ProfessionModel>>> GetAll();
}
