using Fluxor;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Client.Library.Features.State;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Features.Character;

public class CharacterGetAllEffects
{
    private readonly ICharacterManager _characterManager;

    public CharacterGetAllEffects(ICharacterManager characterManager) => _characterManager = characterManager;

    [EffectMethod(typeof(CharacterGetAllAction))]
    public async Task GetAllAsync(IDispatcher dispatcher)
    {
        try
        {
            var characters = await _characterManager.GetAllAsync();

            if (characters.Succeeded is not true || characters.Data is null)
            {
                throw new Exception(characters.Message);
            }

            dispatcher.Dispatch(new CharacterGetAllSuccessAction(characters.Data));
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
        state with { CurrentErrorMessage = null, IsLoading = true };

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
