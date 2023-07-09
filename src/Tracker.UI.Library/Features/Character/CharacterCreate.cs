using Blazored.Toast.Services;
using Fluxor;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Client.Library.Features.State;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Features.Character;

public class CharacterCreateEffects
{
    private readonly ICharacterManager _characterManager;
    private readonly IToastService _toastService;

    public CharacterCreateEffects(ICharacterManager characterManager, IToastService toastService)
    {
        _characterManager = characterManager;
        _toastService = toastService;
    }

    [EffectMethod]
    public async Task CreateAsync(CharacterCreateAction action, IDispatcher dispatcher)
    {
        try
        {
            var character = await _characterManager.CreateAsync(action.Request);

            if (character.Succeeded is not true || character.Data is null)
            {
                throw new Exception(character.Message);
            }

            _toastService.ShowSuccess($"{character.Data.Name} has been created");
            dispatcher.Dispatch(new CharacterCreateSuccessAction(character.Data));
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
        state with { CurrentErrorMessage = null, IsRefreshing = true };

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
