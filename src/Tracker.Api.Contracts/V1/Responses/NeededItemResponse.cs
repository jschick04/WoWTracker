namespace Tracker.Api.Contracts.V1.Responses;

public class NeededItemResponse
{
    /// <summary>Id of the needed item</summary>
    public int Id { get; set; }

    public string CharacterName { get; set; } = null!;

    public string Profession { get; set; } = null!;

    /// <summary>Name of the needed item</summary>
    public string Name { get; set; } = null!;

    public int Amount { get; set; }
}
