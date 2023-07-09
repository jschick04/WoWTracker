using Blazored.Toast.Services;
using Fluxor;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Client.Library.Features.State;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Features.Character;

public class CharacterUpdateSelectedEffects
{
    private readonly ICharacterManager _characterManager;
    private readonly IToastService _toastService;

    public CharacterUpdateSelectedEffects(ICharacterManager characterManager, IToastService toastService)
    {
        _characterManager = characterManager;
        _toastService = toastService;
    }

    [EffectMethod]
    public async Task UpdateSelectedAsync(CharacterUpdateSelectedAction action, IDispatcher dispatcher)
    {
        try
        {
            var result = await _characterManager.UpdateAsync(action.Id, action.Request);

            if (result.Succeeded is not true)
            {
                throw new Exception(result.Message);
            }

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
        state with { CurrentErrorMessage = null, IsRefreshing = true };

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
