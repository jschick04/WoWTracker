using System.Security.Claims;

namespace Tracker.Library.Helpers {

    public static class ClaimsPrincipalExtensions {

        public static string GetId(this ClaimsPrincipal claimsPrincipal) =>
            claimsPrincipal?.FindFirst(ClaimTypes.Name)?.Value;

    }

}