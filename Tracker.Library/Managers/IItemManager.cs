using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Helpers;

namespace Tracker.Library.Managers;

public interface IItemManager
{
    Dictionary<string, List<ItemResponse>>? Items { get; }

    Task<IResult> GetAllAsync();

    Task<Result<List<NeededItemResponse>>> GetCraftableByProfession(string profession);
}
