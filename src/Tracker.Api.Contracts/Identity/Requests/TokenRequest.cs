using System.ComponentModel.DataAnnotations;

namespace Tracker.Api.Contracts.Identity.Requests;

public record TokenRequest
{
    [Required]
    public string Token { get; set; } = null!;
}
