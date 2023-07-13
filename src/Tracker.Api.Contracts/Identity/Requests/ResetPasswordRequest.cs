using System.ComponentModel.DataAnnotations;
using Tracker.Api.Contracts.Attributes;

namespace Tracker.Api.Contracts.Identity.Requests;

public record ResetPasswordRequest
{
    [Required]
    public string Token { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [Password]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare(nameof(Password), ErrorMessage = "Password confirmation does not match Password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;
}
