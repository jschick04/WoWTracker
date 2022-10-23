using Fluxor;
using Tracker.Client.Library.Store.State;

namespace Tracker.Client.Library.Store.Character;

public class CharacterFeature : Feature<CharacterState>
{
    public override string GetName() => "Character";

    protected override CharacterState GetInitialState() => new(true, null, null, null);
}
