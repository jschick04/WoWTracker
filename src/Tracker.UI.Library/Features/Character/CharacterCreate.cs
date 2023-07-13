using System.Net.Http.Json;
using Blazored.Toast.Services;
using Fluxor;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.UI.Library.Features.State;

namespace Tracker.UI.Library.Features.Character;

public class CharacterCreateEffects
{
    private readonly HttpClient _httpClient;
    private readonly IToastService _toastService;

    public CharacterCreateEffects(HttpClient httpClient, IToastService toastService)
    {
        _httpClient = httpClient;
        _toastService = toastService;
    }

    [EffectMethod]
    public async Task CreateAsync(CharacterCreateAction action, IDispatcher dispatcher)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Character.CreateUri, action.Request);

            response.EnsureSuccessStatusCode();

            var character = await response.Content.ReadFromJsonAsync<CharacterResponse>() ??
                throw new Exception($"Error retrieving data from {ApiRoutes.Character.CreateUri}");

            _toastService.ShowSuccess($"{character.Name} has been created");
            dispatcher.Dispatch(new CharacterCreateSuccessAction(character));
        }
        catch (Exception ex)
        {
            _toastService.ShowError(ex.Message);
            dispatcher.Dispatch(new CharacterCreateFailureAction(ex.Message));
        }

        dispatcher.Dispatch(new ClearRefreshingAction());
    }
}

public class CharacterCreateReducers
{
    [ReducerMethod(typeof(CharacterCreateAction))]
    public static CharacterState OnCreateAction(CharacterState state) =>
        state with { CurrentErrorMessage = string.Empty, IsRefreshing = true };

    [ReducerMethod]
    public static CharacterState OnCreateFailureAction(CharacterState state, CharacterCreateFailureAction action) =>
        state with { CurrentErrorMessage = action.ErrorMessage };

    [ReducerMethod]
    public static CharacterState OnCreateSuccessAction(CharacterState state, CharacterCreateSuccessAction action)
    {
        var updatedList = state.Characters.ToList();

        updatedList.Add(action.Character);

        return state with { Characters = updatedList };
    }
}

#region Actions

public record CharacterCreateAction(CreateCharacterRequest Request);

public record CharacterCreateFailureAction(string ErrorMessage);

public record CharacterCreateSuccessAction(CharacterResponse Character);

#endregion
