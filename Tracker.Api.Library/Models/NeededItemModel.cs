using Tracker.Api.Library.Helpers;

namespace Tracker.Api.Library.Models;

public class NeededItemModel
{
    public int Id { get; set; }

    public string CharacterName { get; set; } = null!;

    public Professions ProfessionId { get; set; }

    public string Name { get; set; } = null!;

    public int Amount { get; set; }
}
