using Fluxor;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Client.Library.Features.State;
using Tracker.Library.Helpers;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Features.CraftedItem;

public class CraftedItemGetAllEffects
{
    private readonly IItemManager _itemManager;

    public CraftedItemGetAllEffects(IItemManager itemManager) => _itemManager = itemManager;

    [EffectMethod]
    public async Task GetAllAsync(CraftedItemGetAllAction action, IDispatcher dispatcher)
    {
        List<NeededItemResponse> firstList = new();
        List<NeededItemResponse> secondList = new();

        try
        {
            if (!string.IsNullOrWhiteSpace(action.Primary))
            {
                var first = await _itemManager.GetCraftableByProfession(action.Primary);
                first.GetDataIfSuccess(ref firstList);
            }

            if (!string.IsNullOrWhiteSpace(action.Secondary))
            {
                var second = await _itemManager.GetCraftableByProfession(action.Secondary);
                second.GetDataIfSuccess(ref secondList);
            }

            var itemsToCraft = firstList.Concat(secondList)
                .OrderBy(item => item.Name)
                .ThenByDescending(item => item.Amount);

            dispatcher.Dispatch(new CraftedItemGetAllSuccessAction(itemsToCraft));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new CraftedItemGetAllFailureAction(ex.Message));
        }

        dispatcher.Dispatch(new ClearLoadingAction());
    }
}

public class CraftedItemGetAllReducers
{
    [ReducerMethod(typeof(CraftedItemGetAllAction))]
    public static CraftedItemState OnGetAll(CraftedItemState state) =>
        state with { CurrentErrorMessage = null, IsLoading = true };

    [ReducerMethod]
    public static CraftedItemState OnGetAllFailure(CraftedItemState state, CraftedItemGetAllFailureAction action) =>
        state with { CurrentErrorMessage = action.ErrorMessage };

    [ReducerMethod]
    public static CraftedItemState OnGetAllSuccess(CraftedItemState state, CraftedItemGetAllSuccessAction action) =>
        state with { Items = action.Items };
}

#region Actions

public record CraftedItemGetAllAction(string? Primary, string? Secondary);

public record CraftedItemGetAllFailureAction(string ErrorMessage) : FailureAction(ErrorMessage);

public record CraftedItemGetAllSuccessAction(IEnumerable<NeededItemResponse> Items);

#endregion
