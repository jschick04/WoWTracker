namespace Tracker.Api.Contracts.V1.Responses;

public sealed class NeededItemResponse
{
    public string CharacterId { get; set; } = null!;

    public string CharacterName { get; set; } = null!;

    public string Profession { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Amount { get; set; }
}
