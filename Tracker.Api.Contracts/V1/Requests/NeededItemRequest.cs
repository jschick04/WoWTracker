using System.ComponentModel.DataAnnotations;

namespace Tracker.Api.Contracts.V1.Requests;

public class NeededItemRequest
{
    [Required]
    public string Profession { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    public int Amount { get; set; } = 1;
}
