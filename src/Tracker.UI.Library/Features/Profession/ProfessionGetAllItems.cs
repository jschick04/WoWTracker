using System.Collections.Immutable;
using System.Net.Http.Json;
using Fluxor;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.UI.Library.Features.State;

namespace Tracker.UI.Library.Features.Profession;

public class ProfessionGetAllItemsEffects
{
    private readonly HttpClient _httpClient;

    public ProfessionGetAllItemsEffects(HttpClient httpClient) => _httpClient = httpClient;

    [EffectMethod(typeof(ProfessionGetAllItemsAction))]
    public async Task GetAllItemsAsync(IDispatcher dispatcher)
    {
        try
        {
            var response = await _httpClient.GetAsync(ApiRoutes.Item.GetAllUri);

            response.EnsureSuccessStatusCode();

            var items = await response.Content
                    .ReadFromJsonAsync<ImmutableDictionary<string, ImmutableList<ItemResponse>>>() ??
                throw new Exception($"Error retrieving data from {ApiRoutes.Item.GetAllUri}");

            dispatcher.Dispatch(new ProfessionGetAllItemsActionSuccessAction(items));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new ProfessionGetAllItemsActionFailureAction(ex.Message));
        }

        dispatcher.Dispatch(new ClearLoadingAction());
    }
}

public class ProfessionGetAllItemsReducers
{
    [ReducerMethod(typeof(ProfessionGetAllItemsAction))]
    public static ProfessionState OnGetAllItems(ProfessionState state) =>
        state with { CurrentErrorMessage = string.Empty, IsLoading = true };

    [ReducerMethod]
    public static ProfessionState OnGetAllItemsFailure(ProfessionState state,
        ProfessionGetAllItemsActionFailureAction action) => state with { CurrentErrorMessage = action.ErrorMessage };

    [ReducerMethod]
    public static ProfessionState OnGetAllItemsSuccess(ProfessionState state,
        ProfessionGetAllItemsActionSuccessAction action) => state with { Items = action.Items };
}

#region Actions

public record ProfessionGetAllItemsAction;

public record ProfessionGetAllItemsActionFailureAction(string ErrorMessage) : FailureAction(ErrorMessage);

public record ProfessionGetAllItemsActionSuccessAction(IReadOnlyDictionary<string, ImmutableList<ItemResponse>> Items);

#endregion
