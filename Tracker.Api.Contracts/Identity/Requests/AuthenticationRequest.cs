using System.ComponentModel.DataAnnotations;
using Tracker.Api.Contracts.Helpers;

namespace Tracker.Api.Contracts.Identity.Requests;

public class AuthenticationRequest {

    [Required(ErrorMessage = "Username is required")]
    [EmailAddress]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [Password]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

}