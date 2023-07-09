using Tracker.Api.Library.Helpers;

namespace Tracker.Api.Library.Models;

public class NeededItemModel
{
    /// <summary>Id of character that needs this item</summary>
    public int Id { get; set; }

    public string CharacterName { get; set; } = string.Empty;

    public Professions ProfessionId { get; set; }

    /// <summary>Name of the item</summary>
    public string Name { get; set; } = null!;

    public int Amount { get; set; }
}
