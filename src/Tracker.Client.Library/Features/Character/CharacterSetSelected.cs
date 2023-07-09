using Fluxor;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Client.Library.Features.CraftedItem;
using Tracker.Client.Library.Features.State;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Features.Character;

public class CharacterSetSelectedEffects
{
    private readonly ICharacterManager _characterManager;

    public CharacterSetSelectedEffects(ICharacterManager characterManager) => _characterManager = characterManager;

    [EffectMethod]
    public async Task SetSelectedAsync(CharacterSetSelectedAction action, IDispatcher dispatcher)
    {
        try
        {
            var character = await _characterManager.GetByIdAsync(action.Id);

            if (character.Succeeded is not true || character.Data is null)
            {
                throw new Exception(character.Message);
            }

            dispatcher.Dispatch(new CharacterSetSelectedSuccessAction(character.Data));

            dispatcher.Dispatch(new CraftedItemGetAllAction(character.Data.FirstProfession,
                character.Data.SecondProfession));
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
        state with { CurrentErrorMessage = null, IsRefreshing = true };

    [ReducerMethod]
    public static CharacterState OnSetSelectedFailure(CharacterState state, CharacterSetSelectedFailureAction action) =>
        state with { CurrentErrorMessage = action.ErrorMessage };

    [ReducerMethod]
    public static CharacterState OnSetSelectedSuccess(CharacterState state, CharacterSetSelectedSuccessAction action) =>
        state with { Selected = action.Character };
}

#region Actions

public record CharacterSetSelectedAction(int Id);

public record CharacterSetSelectedFailureAction(string ErrorMessage) : FailureAction(ErrorMessage);

public record CharacterSetSelectedSuccessAction(CharacterResponse Character);

#endregion
