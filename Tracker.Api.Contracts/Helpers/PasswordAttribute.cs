using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Tracker.Api.Contracts.Helpers;

public class PasswordAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) { return ValidationResult.Success!; }

        string? password = value.ToString();

        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
        {
            return new ValidationResult("Password must be at least 8 characters long");
        }

        if (!hasNumber.IsMatch(password))
        {
            return new ValidationResult("Password must contain at least one number");
        }

        if (!hasUpperChar.IsMatch(password))
        {
            return new ValidationResult("Password must contain at least one upper case character");
        }

        if (!hasLowerChar.IsMatch(password))
        {
            return new ValidationResult("Password must contain at least one lower case character");
        }

        if (!hasSymbols.IsMatch(password))
        {
            return new ValidationResult(
                "Password must contain at least one special character - !@#$%^&*()_+=[{]};:<>|./?,-"
            );
        }

        return ValidationResult.Success!;
    }
}
