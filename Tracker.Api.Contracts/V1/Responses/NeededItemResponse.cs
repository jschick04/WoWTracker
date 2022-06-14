namespace Tracker.Api.Contracts.V1.Responses;

public class NeededItemResponse {

    public int Id { get; set; }

    public string CharacterName { get; set; } = null!;

    public string Profession { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Amount { get; set; }

}