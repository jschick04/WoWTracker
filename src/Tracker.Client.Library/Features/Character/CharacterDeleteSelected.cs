using Blazored.Toast.Services;
using Fluxor;
using Tracker.Client.Library.Features.State;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Features.Character;

public class CharacterDeleteSelectedEffects
{
    private readonly ICharacterManager _characterManager;
    private readonly IToastService _toastService;

    public CharacterDeleteSelectedEffects(ICharacterManager characterManager, IToastService toastService)
    {
        _characterManager = characterManager;
        _toastService = toastService;
    }

    [EffectMethod]
    public async Task DeleteSelectedAsync(CharacterDeleteSelectedAction action, IDispatcher dispatcher)
    {
        try
        {
            var result = await _characterManager.DeleteAsync(action.Id);

            if (result.Succeeded is not true)
            {
                throw new Exception(result.Message);
            }

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
    public static CharacterState OnDeleteSelected(CharacterState state) => state with { CurrentErrorMessage = null };

    [ReducerMethod]
    public static CharacterState OnDeleteSelectedFailure(CharacterState state,
        CharacterDeleteSelectedFailureAction action) => state with { CurrentErrorMessage = action.ErrorMessage };

    [ReducerMethod]
    public static CharacterState OnDeleteSelectedSuccess(CharacterState state,
        CharacterDeleteSelectedSuccessAction action)
    {
        var updatedList = state.Characters.Where(c => c.Id != action.Id).ToList();

        return state with { Characters = updatedList, Selected = null };
    }
}

#region Actions

public record CharacterDeleteSelectedAction(int Id);

public record CharacterDeleteSelectedFailureAction(string ErrorMessage) : FailureAction(ErrorMessage);

public record CharacterDeleteSelectedSuccessAction(int Id);

#endregion
