using System.ComponentModel.DataAnnotations;

namespace Tracker.Api.Contracts.Identity.Requests;

public sealed record TokenRequest
{
    [Required]
    public string Token { get; set; } = null!;
}
