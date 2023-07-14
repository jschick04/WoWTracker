using Blazored.Toast.Services;
using Fluxor;
using Tracker.Api.Contracts.Routes;
using Tracker.UI.Library.Features.State;

namespace Tracker.UI.Library.Features.Character;

public class CharacterDeleteSelectedEffects
{
    private readonly HttpClient _httpClient;
    private readonly IToastService _toastService;

    public CharacterDeleteSelectedEffects(HttpClient httpClient, IToastService toastService)
    {
        _httpClient = httpClient;
        _toastService = toastService;
    }

    [EffectMethod]
    public async Task DeleteSelectedAsync(CharacterDeleteSelectedAction action, IDispatcher dispatcher)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(ApiRoutes.Character.Delete(action.Id));

            response.EnsureSuccessStatusCode();

            _toastService.ShowSuccess("Delete Successful");
            dispatcher.Dispatch(new CharacterDeleteSelectedSuccessAction(action.Id));
        }
        catch (Exception ex)
        {
            _toastService.ShowError(ex.Message);
            dispatcher.Dispatch(new CharacterDeleteSelectedFailureAction(ex.Message));
        }
    }
}

public class CharacterDeleteSelectedReducers
{
    [ReducerMethod(typeof(CharacterDeleteSelectedAction))]
    public static CharacterState OnDeleteSelected(CharacterState state) =>
        state with { CurrentErrorMessage = string.Empty };

    [ReducerMethod]
    public static CharacterState OnDeleteSelectedFailure(CharacterState state,
        CharacterDeleteSelectedFailureAction action) => state with { CurrentErrorMessage = action.ErrorMessage };

    [ReducerMethod]
    public static CharacterState OnDeleteSelectedSuccess(CharacterState state,
        CharacterDeleteSelectedSuccessAction action)
    {
        var updatedList = state.Characters.Where(c => c.Id != action.Id);

        return state with { Characters = updatedList, Selected = null };
    }
}

#region Actions

public record CharacterDeleteSelectedAction(string Id);

public record CharacterDeleteSelectedFailureAction(string ErrorMessage) : FailureAction(ErrorMessage);

public record CharacterDeleteSelectedSuccessAction(string Id);

#endregion
