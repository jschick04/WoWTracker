using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Tracker.Api.Contracts.Attributes;

public sealed partial class PasswordAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        string? password = value?.ToString();

        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
        {
            return new ValidationResult("Password must be at least 8 characters long");
        }

        if (!HasNumber().IsMatch(password))
        {
            return new ValidationResult("Password must contain at least one number");
        }

        if (!HasUpperChar().IsMatch(password))
        {
            return new ValidationResult("Password must contain at least one upper case character");
        }

        if (!HasLowerChar().IsMatch(password))
        {
            return new ValidationResult("Password must contain at least one lower case character");
        }

        if (!HasSymbol().IsMatch(password))
        {
            return new ValidationResult(
                "Password must contain at least one special character - !@#$%^&*()_+=[{]};:<>|./?,-"
            );
        }

        return ValidationResult.Success;
    }

    [GeneratedRegex("[a-z]+")]
    private static partial Regex HasLowerChar();

    [GeneratedRegex("[0-9]+")]
    private static partial Regex HasNumber();

    [GeneratedRegex("[!@#$%^&*()_+=\\[{\\]};:<>|./?,-]")]
    private static partial Regex HasSymbol();

    [GeneratedRegex("[A-Z]+")]
    private static partial Regex HasUpperChar();
}
