using System.Net.Http.Json;
using Fluxor;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.UI.Library.Features.State;

namespace Tracker.UI.Library.Features.CraftedItem;

public class CraftedItemGetAllEffects
{
    private readonly HttpClient _httpClient;

    public CraftedItemGetAllEffects(HttpClient httpClient) => _httpClient = httpClient;

    [EffectMethod]
    public async Task GetAllAsync(CraftedItemGetAllAction action, IDispatcher dispatcher)
    {
        List<NeededItemResponse> firstList = new();
        List<NeededItemResponse> secondList = new();

        try
        {
            if (!string.IsNullOrWhiteSpace(action.Primary))
            {
                var first = await _httpClient.GetAsync(ApiRoutes.Item.GetCraftableByProfession(action.Primary));

                first.EnsureSuccessStatusCode();

                firstList = await first.Content.ReadFromJsonAsync<List<NeededItemResponse>>() ??
                    throw new Exception($"Error retrieving data from " +
                        $"{ApiRoutes.Item.GetCraftableByProfession(action.Primary)}");
            }

            if (!string.IsNullOrWhiteSpace(action.Secondary))
            {
                var second = await _httpClient.GetAsync(ApiRoutes.Item.GetCraftableByProfession(action.Secondary));

                second.EnsureSuccessStatusCode();

                secondList = await second.Content.ReadFromJsonAsync<List<NeededItemResponse>>() ??
                    throw new Exception($"Error retrieving data from " +
                        $"{ApiRoutes.Item.GetCraftableByProfession(action.Secondary)}");
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
        state with { CurrentErrorMessage = string.Empty, IsLoading = true };

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
