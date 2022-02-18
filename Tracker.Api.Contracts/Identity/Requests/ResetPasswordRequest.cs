using System.ComponentModel.DataAnnotations;
using Tracker.Api.Contracts.Helpers;

namespace Tracker.Api.Contracts.Identity.Requests;

public class ResetPasswordRequest {

    [Required]
    public string Token { get; set; } = null!;

    [Required]
    [Password]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [Compare(nameof(Password))]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;

}