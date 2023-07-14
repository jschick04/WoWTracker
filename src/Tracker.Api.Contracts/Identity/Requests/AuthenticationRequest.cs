using System.ComponentModel.DataAnnotations;
using Tracker.Api.Contracts.Attributes;

namespace Tracker.Api.Contracts.Identity.Requests;

public sealed record AuthenticationRequest
{
    [Required(ErrorMessage = "Username is required")]
    [EmailAddress]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [Password]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
