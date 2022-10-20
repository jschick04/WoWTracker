using System.ComponentModel.DataAnnotations;

namespace Tracker.Api.Contracts.Identity.Requests;

public class ForgotPasswordRequest
{
    [Required(ErrorMessage = "Please enter a valid username")]
    [EmailAddress]
    public string Username { get; set; } = null!;
}
