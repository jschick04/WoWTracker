using System.Security.Claims;

namespace Tracker.Library.Helpers;

public static class ClaimsPrincipalExtensions
{
    public static string GetFirstName(this ClaimsPrincipal claimsPrincipal)
    {
        var firstName = claimsPrincipal?.FindFirst(ClaimTypes.GivenName)?.Value;

        if (firstName is null)
        {
            throw new ApplicationException("Failed to retrieve first name from claims");
        }

        return firstName;
    }

    public static int GetId(this ClaimsPrincipal claimsPrincipal)
    {
        var claimId = claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(claimId, out var id))
        {
            throw new ApplicationException("Failed to retrieve ID from claims");
        }

        return id;
    }

    public static string GetLastName(this ClaimsPrincipal claimsPrincipal)
    {
        var lastName = claimsPrincipal?.FindFirst(ClaimTypes.Surname)?.Value;

        if (lastName is null)
        {
            throw new ApplicationException("Failed to retrieve last name from claims");
        }

        return lastName;
    }

    public static string GetRole(this ClaimsPrincipal claimsPrincipal)
    {
        var role = claimsPrincipal?.FindFirst(ClaimTypes.Role)?.Value;

        if (role is null)
        {
            throw new ApplicationException("Failed to retrieve role from claims");
        }

        return role;
    }

    public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
    {
        var username = claimsPrincipal?.FindFirst(ClaimTypes.Name)?.Value;

        if (username is null)
        {
            throw new ApplicationException("Failed to retrieve username from claims");
        }

        return username;
    }
}
