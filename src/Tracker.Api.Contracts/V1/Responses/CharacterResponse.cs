namespace Tracker.Api.Contracts.V1.Responses;

public sealed class CharacterResponse
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Class { get; set; } = null!;

    public string? FirstProfession { get; set; }

    public string? SecondProfession { get; set; }

    public bool HasCooking { get; set; }
}
