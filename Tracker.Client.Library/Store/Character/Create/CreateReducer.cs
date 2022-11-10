﻿using Fluxor;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Client.Library.Store.State;

namespace Tracker.Client.Library.Store.Character.Create;

public class CreateReducer
{
    [ReducerMethod(typeof(CreateAction))]
    public static CharacterState ReducerCreateAction(CharacterState state) =>
        new(false, null, state.Characters, state.Selected, true);

    [ReducerMethod]
    public static CharacterState ReducerCreateFailureAction(CharacterState state, CreateFailureAction action) =>
        new(false, action.ErrorMessage, state.Characters, state.Selected);

    [ReducerMethod]
    public static CharacterState ReducerCreateSuccessAction(CharacterState state, CreateSuccessAction action)
    {
        if (state.Characters.Any() is false)
        {
            return new CharacterState(false,
                null,
                new List<CharacterResponse> { action.Character },
                null);
        }

        var updatedList = state.Characters.ToList();
        updatedList.Add(action.Character);

        return new CharacterState(false, null, updatedList, state.Selected);
    }
}