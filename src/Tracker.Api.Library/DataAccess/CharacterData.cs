using FluentResults;
using Tracker.Api.Library.Database;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess;

public sealed class CharacterData : ICharacterData
{
    private readonly ISqlDataAccess _db;

    public CharacterData(ISqlDataAccess db) => _db = db;

    public async Task<Result> AddNeededItem(NeededItemModel model)
    {
        try
        {
            await _db.SaveData("spNeededItems_Add", model);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<IResult<int>> Create(CharacterModel model)
    {
        try
        {
            return Result.Ok(await _db.SaveData<int, CharacterModel>("spCharacters_Insert", model));
        }
        catch (Exception ex)
        {
            return Result.Fail<int>(ex.Message);
        }
    }

    public async Task<Result> Delete(int id)
    {
        try
        {
            await _db.SaveData("spCharacters_Delete", new { id });

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<IResult<IEnumerable<CharacterModel>>> GetAll(int userId)
    {
        try
        {
            return Result.Ok(await _db.LoadData<CharacterModel, dynamic>("spCharacters_GetAll", new { userId }));
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<CharacterModel>>(ex.Message);
        }
    }

    public async Task<IResult<CharacterModel?>> GetById(int id, int userId)
    {
        try
        {
            var result = await _db.LoadData<CharacterModel, dynamic>("spCharacters_GetById", new { id, userId });

            return Result.Ok(result.FirstOrDefault());
        }
        catch (Exception ex)
        {
            return Result.Fail<CharacterModel?>(ex.Message);
        }
    }

    public async Task<IResult<IEnumerable<NeededItemModel>>> GetNeededItems(int id)
    {
        try
        {
            return Result.Ok(
                await _db.LoadData<NeededItemModel, dynamic>("spNeededItems_GetByCharacterId", new { id }));
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<NeededItemModel>>(ex.Message);
        }
    }

    public async Task<Result> RemoveNeededItem(NeededItemModel model)
    {
        try
        {
            await _db.SaveData("spNeededItems_Remove", model);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result> Update(CharacterModel model)
    {
        try
        {
            await _db.SaveData("spCharacters_Update", model);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
