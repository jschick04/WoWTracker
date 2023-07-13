using System.Net.Http.Json;
using Fluxor;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.UI.Library.Features.State;

namespace Tracker.UI.Library.Features.NeededItem;

public class NeededItemGetAllEffects
{
    private readonly HttpClient _httpClient;

    public NeededItemGetAllEffects(HttpClient httpClient) => _httpClient = httpClient;

    [EffectMethod]
    public async Task GetAllAsync(NeededItemGetAllAction action, IDispatcher dispatcher)
    {
        try
        {
            var response = await _httpClient.GetAsync(ApiRoutes.Character.GetNeededItems(action.Id));

            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<List<NeededItemResponse>>() ??
                throw new Exception($"Error retrieving data from {ApiRoutes.Character.GetNeededItems(action.Id)}");

            dispatcher.Dispatch(new NeededItemGetAllSuccessAction(items));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new NeededItemGetAllFailureAction(ex.Message));
        }

        dispatcher.Dispatch(new ClearLoadingAction());
    }
}

public class NeededItemGetAllReducers
{
    [ReducerMethod(typeof(NeededItemGetAllAction))]
    public static NeededItemState OnGetAll(NeededItemState state) =>
        state with { CurrentErrorMessage = string.Empty, IsLoading = true };

    [ReducerMethod]
    public static NeededItemState OnGetAllFailure(NeededItemState state, NeededItemGetAllFailureAction action) =>
        state with { CurrentErrorMessage = action.ErrorMessage };

    [ReducerMethod]
    public static NeededItemState OnGetAllSuccess(NeededItemState state, NeededItemGetAllSuccessAction action) =>
        state with { Items = action.Items };
}

#region Actions

public record NeededItemGetAllAction(int Id);

public record NeededItemGetAllFailureAction(string ErrorMessage) : FailureAction(ErrorMessage);

public record NeededItemGetAllSuccessAction(IEnumerable<NeededItemResponse> Items);

#endregion
