using FluentResults;
using Tracker.Api.Library.Database;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess;

public sealed class ItemData : IItemData
{
    private readonly ISqlDataAccess _db;

    public ItemData(ISqlDataAccess db) => _db = db;

    public async Task<IResult<IEnumerable<ItemModel>>> GetByProfession(string profession)
    {
        try
        {
            return Result.Ok(
                await _db.LoadData<ItemModel, dynamic>("spItems_GetByProfession", new { name = profession }));
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<ItemModel>>(ex.Message);
        }
    }

    public async Task<IResult<IEnumerable<ItemModel>>> GetBySlot(string slot)
    {
        try
        {
            return Result.Ok(await _db.LoadData<ItemModel, dynamic>("spItems_GetBySlot", new { name = slot }));
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<ItemModel>>(ex.Message);
        }
    }

    public async Task<IResult<IEnumerable<CraftableItemModel>>> GetCraftableByProfession(int userId, int professionId)
    {
        try
        {
            return Result.Ok(
                await _db.LoadData<CraftableItemModel, dynamic>("spNeededItems_GetCraftableByProfession",
                    new { userId, professionId }));
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<CraftableItemModel>>(ex.Message);
        }
    }
}
