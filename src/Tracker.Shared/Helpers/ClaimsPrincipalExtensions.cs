using System.Security.Claims;

namespace Tracker.Shared.Helpers;

public static class ClaimsPrincipalExtensions
{
    public static string GetFirstName(this ClaimsPrincipal claimsPrincipal)
    {
        var firstName = claimsPrincipal.FindFirst(ClaimTypes.GivenName)?.Value;

        return firstName ?? throw new ApplicationException("Failed to retrieve first name from claims");
    }

    public static int GetId(this ClaimsPrincipal claimsPrincipal)
    {
        var claimId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(claimId, out var id))
        {
            throw new ApplicationException("Failed to retrieve ID from claims");
        }

        return id;
    }

    public static string GetLastName(this ClaimsPrincipal claimsPrincipal)
    {
        var lastName = claimsPrincipal.FindFirst(ClaimTypes.Surname)?.Value;

        return lastName ?? throw new ApplicationException("Failed to retrieve last name from claims");
    }

    public static string GetRole(this ClaimsPrincipal claimsPrincipal)
    {
        var role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;

        return role ?? throw new ApplicationException("Failed to retrieve role from claims");
    }

    public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
    {
        var username = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

        return username ?? throw new ApplicationException("Failed to retrieve username from claims");
    }
}
