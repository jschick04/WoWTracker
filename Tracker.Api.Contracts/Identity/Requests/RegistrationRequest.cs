using System.ComponentModel.DataAnnotations;
using Tracker.Api.Contracts.Helpers;

namespace Tracker.Api.Contracts.Identity.Requests;

public class RegistrationRequest {

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Username { get; set; } = null!;

    [Required]
    [Password]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [Compare(nameof(Password))]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;

    [Range(typeof(bool), "true", "true")]
    public bool AcceptTerms { get; set; }

}