using FluentResults;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Library.DataAccess;

public interface IItemData
{
    Task<IResult<IEnumerable<ItemModel>>> GetByProfession(string profession);

    Task<IResult<IEnumerable<ItemModel>>> GetBySlot(string slot);

    Task<IResult<IEnumerable<CraftableItemModel>>> GetCraftableByProfession(int userId, int professionId);
}
