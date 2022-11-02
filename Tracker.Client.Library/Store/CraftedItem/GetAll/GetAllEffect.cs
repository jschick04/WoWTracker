using Fluxor;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Helpers;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Store.CraftedItem.GetAll;

public class GetAllEffect : Effect<GetAllAction>
{
    private readonly IItemManager _itemManager;

    public GetAllEffect(IItemManager itemManager) => _itemManager = itemManager;

    public override async Task HandleAsync(GetAllAction action, IDispatcher dispatcher)
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

            dispatcher.Dispatch(new GetAllSuccessAction(itemsToCraft));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new GetAllFailureAction(ex.Message));
        }
    }
}
