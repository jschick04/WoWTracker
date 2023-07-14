using System.Net.Http.Json;
using Fluxor;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.UI.Library.Features.CraftedItem;
using Tracker.UI.Library.Features.State;

namespace Tracker.UI.Library.Features.Character;

public class CharacterSetSelectedEffects
{
    private readonly HttpClient _httpClient;

    public CharacterSetSelectedEffects(HttpClient httpClient) => _httpClient = httpClient;

    [EffectMethod]
    public async Task SetSelectedAsync(CharacterSetSelectedAction action, IDispatcher dispatcher)
    {
        try
        {
            var response = await _httpClient.GetAsync(ApiRoutes.Character.GetById(action.Id));

            response.EnsureSuccessStatusCode();

            var character = await response.Content.ReadFromJsonAsync<CharacterResponse>() ??
                throw new Exception($"Error retrieving data from " +
                    $"{ApiRoutes.Character.GetById(action.Id)}");

            dispatcher.Dispatch(new CharacterSetSelectedSuccessAction(character));

            dispatcher.Dispatch(new CraftedItemGetAllAction(character.FirstProfession,
                character.SecondProfession));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new CharacterSetSelectedFailureAction(ex.Message));
        }

        dispatcher.Dispatch(new ClearRefreshingAction());
    }
}

public class CharacterSetSelectedReducers
{
    [ReducerMethod(typeof(CharacterSetSelectedAction))]
    public static CharacterState OnSetSelected(CharacterState state) =>
        state with { CurrentErrorMessage = string.Empty, IsRefreshing = true };

    [ReducerMethod]
    public static CharacterState OnSetSelectedFailure(CharacterState state, CharacterSetSelectedFailureAction action) =>
        state with { CurrentErrorMessage = action.ErrorMessage };

    [ReducerMethod]
    public static CharacterState OnSetSelectedSuccess(CharacterState state, CharacterSetSelectedSuccessAction action) =>
        state with { Selected = action.Character };
}

#region Actions

public record CharacterSetSelectedAction(string Id);

public record CharacterSetSelectedFailureAction(string ErrorMessage) : FailureAction(ErrorMessage);

public record CharacterSetSelectedSuccessAction(CharacterResponse Character);

#endregion
