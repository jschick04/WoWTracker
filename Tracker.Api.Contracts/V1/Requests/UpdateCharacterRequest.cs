namespace Tracker.Api.Contracts.V1.Requests;

public class UpdateCharacterRequest {

    private string? _name;
    private string? _class;
    private string? _firstProfession;
    private string? _secondProfession;

    public string? Name {
        get => _name;
        set => _name = ReplaceEmptyWithNull(value);
    }

    public string? Class {
        get => _class;
        set => _class = ReplaceEmptyWithNull(value);
    }

    public string? FirstProfession {
        get => _firstProfession;
        set => _firstProfession = ReplaceEmptyWithNull(value);
    }

    public string? SecondProfession {
        get => _secondProfession;
        set => _secondProfession = ReplaceEmptyWithNull(value);
    }

    public bool? HasCooking { get; set; }

    private static string? ReplaceEmptyWithNull(string? value) => string.IsNullOrWhiteSpace(value) ? null : value;

}