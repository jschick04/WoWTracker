using System.Net.Http.Json;
using Fluxor;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.UI.Library.Features.State;

namespace Tracker.UI.Library.Features.Character;

public class CharacterGetAllEffects
{
    private readonly HttpClient _httpClient;

    public CharacterGetAllEffects(HttpClient httpClient) => _httpClient = httpClient;

    [EffectMethod(typeof(CharacterGetAllAction))]
    public async Task GetAllAsync(IDispatcher dispatcher)
    {
        try
        {
            var response = await _httpClient.GetAsync(ApiRoutes.Character.GetAllUri);

            response.EnsureSuccessStatusCode();

            var characters = await response.Content.ReadFromJsonAsync<List<CharacterResponse>>() ??
                throw new Exception($"Error retrieving data from {ApiRoutes.Character.GetAllUri}");

            dispatcher.Dispatch(new CharacterGetAllSuccessAction(characters));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new CharacterGetAllFailureAction(ex.Message));
        }

        dispatcher.Dispatch(new ClearLoadingAction());
    }
}

public class CharacterGetAllReducers
{
    [ReducerMethod(typeof(CharacterGetAllAction))]
    public static CharacterState OnGetAll(CharacterState state) =>
        state with { CurrentErrorMessage = string.Empty, IsLoading = true };

    [ReducerMethod]
    public static CharacterState OnGetAllFailure(CharacterState state, CharacterGetAllFailureAction action) =>
        state with { CurrentErrorMessage = action.ErrorMessage };

    [ReducerMethod]
    public static CharacterState OnGetAllSuccess(CharacterState state, CharacterGetAllSuccessAction action) =>
        state with { Characters = action.Characters };
}

#region Actions

public record CharacterGetAllAction;

public record CharacterGetAllFailureAction(string ErrorMessage) : FailureAction(ErrorMessage);

public record CharacterGetAllSuccessAction(IEnumerable<CharacterResponse> Characters);

#endregion
