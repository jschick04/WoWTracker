using System.ComponentModel.DataAnnotations;
using Tracker.Api.Contracts.Attributes;

namespace Tracker.Api.Contracts.Identity.Requests;

public sealed record RegistrationRequest
{
    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Username is required")]
    [EmailAddress]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [Password]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare(nameof(Password), ErrorMessage = "Password confirmation does not match Password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;

    [Range(typeof(bool), "true", "true", ErrorMessage = "Terms of Use must be accepted")]
    public bool AcceptTerms { get; set; }
}
