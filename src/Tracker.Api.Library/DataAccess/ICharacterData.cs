using FluentResults;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess;

public interface ICharacterData
{
    Task<Result> AddNeededItem(NeededItemModel model);

    Task<IResult<int>> Create(CharacterModel model);

    Task<Result> Delete(int id);

    Task<IResult<IEnumerable<CharacterModel>>> GetAll(int userId);

    Task<IResult<CharacterModel?>> GetById(int id, int userId);

    Task<IResult<IEnumerable<NeededItemModel>>> GetNeededItems(int id);

    Task<Result> RemoveNeededItem(NeededItemModel model);

    Task<Result> Update(CharacterModel model);
}
