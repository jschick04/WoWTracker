using System.Net.Http.Json;
using Blazored.Toast.Services;
using Fluxor;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.UI.Library.Features.State;

namespace Tracker.UI.Library.Features.Character;

public class CharacterUpdateSelectedEffects
{
    private readonly HttpClient _httpClient;
    private readonly IToastService _toastService;

    public CharacterUpdateSelectedEffects(HttpClient httpClient, IToastService toastService)
    {
        _httpClient = httpClient;
        _toastService = toastService;
    }

    [EffectMethod]
    public async Task UpdateSelectedAsync(CharacterUpdateSelectedAction action, IDispatcher dispatcher)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Character.Update(action.Id), action.Request);

            response.EnsureSuccessStatusCode();

            _toastService.ShowSuccess($"{action.Request.Name} has been updated");
            dispatcher.Dispatch(new CharacterUpdateSelectedSuccessAction(action.Id, action.Request));
        }
        catch (Exception ex)
        {
            _toastService.ShowError(ex.Message);
            dispatcher.Dispatch(new CharacterUpdateSelectedFailureAction(ex.Message));
        }

        dispatcher.Dispatch(new ClearRefreshingAction());
    }
}

public class CharacterUpdateSelectedReducers
{
    [ReducerMethod(typeof(CharacterUpdateSelectedAction))]
    public static CharacterState OnUpdateSelected(CharacterState state) =>
        state with { CurrentErrorMessage = string.Empty, IsRefreshing = true };

    [ReducerMethod]
    public static CharacterState OnUpdateSelectedFailure(CharacterState state,
        CharacterUpdateSelectedFailureAction action) => state with { CurrentErrorMessage = action.ErrorMessage };

    [ReducerMethod]
    public static CharacterState OnUpdateSelectedSuccess(CharacterState state,
        CharacterUpdateSelectedSuccessAction action)
    {
        var updatedCharacter = state.Characters.FirstOrDefault(c => c.Id == action.Id);

        if (updatedCharacter is null) { return state; }

        updatedCharacter.Name = action.Request.Name;
        updatedCharacter.Class = action.Request.Class;
        updatedCharacter.FirstProfession = action.Request.FirstProfession;
        updatedCharacter.SecondProfession = action.Request.SecondProfession;
        updatedCharacter.HasCooking = action.Request.HasCooking;

        return state with { Selected = updatedCharacter };
    }
}

#region Actions

public record CharacterUpdateSelectedAction(int Id, UpdateCharacterRequest Request);

public record CharacterUpdateSelectedFailureAction(string ErrorMessage) : FailureAction(ErrorMessage);

public record CharacterUpdateSelectedSuccessAction(int Id, UpdateCharacterRequest Request);

#endregion
