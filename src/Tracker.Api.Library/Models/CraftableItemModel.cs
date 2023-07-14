using Tracker.Shared.Helpers;

namespace Tracker.Api.Library.Models;

public sealed record CraftableItemModel(
    int CharacterId,
    string CharacterName,
    Professions ProfessionId,
    string Name,
    int Amount);
