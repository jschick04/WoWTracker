using Tracker.Api.Library.Helpers;

namespace Tracker.Api.Library.Models;

public sealed record NeededItemModel(
    int CharacterId,
    string CharacterName,
    Professions ProfessionId,
    string Name,
    int Amount);
