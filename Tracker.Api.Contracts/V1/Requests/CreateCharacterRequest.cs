using System.ComponentModel.DataAnnotations;

namespace Tracker.Api.Contracts.V1.Requests;

public class CreateCharacterRequest : BaseRequest {

    private string? _firstProfession;
    private string? _secondProfession;

    public int UserId { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Class { get; set; } = null!;

    public string? FirstProfession {
        get => _firstProfession;
        set => _firstProfession = ReplaceEmptyWithNull(value);
    }

    public string? SecondProfession {
        get => _secondProfession;
        set => _secondProfession = ReplaceEmptyWithNull(value);
    }

    public bool HasCooking { get; set; }

}