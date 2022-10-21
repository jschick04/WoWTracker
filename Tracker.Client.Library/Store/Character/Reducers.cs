using Fluxor;

namespace Tracker.Client.Library.Store.Character;

public class Reducers
{
    [ReducerMethod(typeof(FetchDataAction))]
    public static CharacterState ReducerFetchDataAction(CharacterState state) => new(true, null);

    [ReducerMethod]
    public static CharacterState ReducerFetchDataResultAction(CharacterState state, FetchDataResultAction action) =>
        new(false, action.Characters);
}
