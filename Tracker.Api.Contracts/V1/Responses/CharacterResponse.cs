namespace Tracker.Api.Contracts.V1.Responses;

public class CharacterResponse {

    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Class { get; set; }

    public string? FirstProfession { get; set; }

    public string? SecondProfession { get; set; }

    public bool HasCooking { get; set; }

}